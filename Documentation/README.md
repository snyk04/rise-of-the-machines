# 1. Реалізація пошуку шляхів для ігрового штучного інтелекту за допомогою NavMesh (Сниченков Д. А.)

Іноді нам потрібно, щоб АІ-персонажі бродили по ігровому рівню, слідуючи по грубо окресленому або точно заданому шляху. Наприклад в гоночній грі АІ-противники повинні їхати по дорозі, а в RTS юніти повинні вміти переміщатися в потрібну точку, рухаючись по рельєфу, а також враховувати положення один одного.

Уникнення перешкод - це проста поведінка, що дозволяє АІ-сутностям досягати цільових точок. Важливо зауважити, що основа поведінки нашої АІ-сутності є уникнення перешкод і досягнення мети (гравця). Ця проста поведінка визначає найбільш ефективний і найкоротший шлях.

Для пошуку шляхів АІ сцена повинна бути представлена в певному форматі; на 2D-карті використовується двомірна сітка (масив) для пошуку шляхів алгоритмом A*. АІ-агенти повинні знати, де є перешкоди, особливо статичні. Робота з униканням колізій між динамічно рухомими об'єктами - це інше питання, який зазвичай називається steering behavior. В Unity є вбудований інструмент для генерації NavMesh, що представляє сцену в контексті, зручному для пошуку АІ-агентами оптимального шляху до цілі.

По-перше, слід позначити всю геометрію в сцені, запікаєму в NavMesh, як Navigation Static, по-друге, слід зробити ігрові об'єкти статичними (поставити прапорець Static для всіх їх властивостей)

## Запікання навігаційного меша

Вікно Navmesh має 4 вкладки:
-Agents
-Areas
-Bake
-Object

Вкладка Agents: має в собі налаштування самих АІ-агентів (радіус і висота агента, висота перешкоди яку може подолати агент, ім'я агента, максимальний кут на який може підніматись агент)

Вкладка Areas: Unity має кілька типів областей, які неможливо змінювати: Walkable, Not Walkable і Jump. Також є можливість присвоєння назв і створення нових областей.
Області (Areas) служать двом цілям: робити області доступними або недоступними для агента, а також позначати області як менш бажані з точки зору витрат на переміщення. 

Третя вкладка Bake - напевно, найважливіша. Вона дозволяє створювати сам NavMesh для сцени.
Параметри розміру агента на цій вкладці визначають, як агенти взаємодіятимуть із середовищем, в той час як параметри у вкладці Agents керують взаємодією з іншими агентами і рухомими об'єктами
Натиснувши кнопку Bake ми згенеруємо NavMesh на нашому ігровому рівні.

Остання вкладка Object: три кнопки - All, Mesh Renderers і Terrains - використовуються як фільтри сцени. Вони корисні при роботі в складних сценах з безліччю об'єктів в ієрархії.

## Застосування Nav Mesh Agent
Тепер, коли ми налаштували сцену з NavMesh, нам потрібен спосіб, яким агент зможе використовувати цю інформацію. На щастя для нас, в Unity є компонент Nav Mesh Agent, який можна перетягнути на персонажа. На нашій сцені є ігровий об'єкт з назвою Player, до якого вже прикріплений компонент.
Цей компонент виконує за нас 90% важкої роботи: прокладання шляху, уникнення перешкод і так далі. Єдине, що потрібно - передати агенту цільову точку.

## Задання цільової точки

Після налаштування AI-агента нам потрібен спосіб повідомити йому, куди рухатися. У нашому прикладі проекту є скрипт з назвою Target.cs, що виконує саме цю задачу.

Це простий клас, який виконує три дії:
1.«Вистрілює» промінь з камери до позиції миші в світі
2.Оновлює позицію маркера
3.Оновлює властивість точки призначення для всіх агентів NavMesh

Весь клас виглядає наступним чином:

```cs
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    private NavMeshAgent[] navAgents;
    public Transform targetMarker;

    private void Start()
    {
      navAgents = FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[];
    }

    private void UpdateTargets(Vector3 targetPosition)
    {
      foreach(NavMeshAgent agent in navAgents) 
      {
        agent.destination = targetPosition;
      }
    }

    private void Update()
    {
        if (GetInput()) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo)) 
            {
                Vector3 targetPosition = hitInfo.point;
                UpdateTargets(targetPosition);
                targetMarker.position = targetPosition;
            }
        }
    }

    private bool GetInput() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos() 
    {
        Debug.DrawLine(targetMarker.position, targetMarker.position + Vector3.up * 5, Color.red);
    }
}
```

Тут відбуваються такі дії: в методі Start ми ініуіюємо масив navAgents за допомогою методу FindObjectsOfType().

Метод UpdateTargets() проходить по нашому масиву navAgents і задає для них цільову точку в заданому Vector3. Це ключ до роботи коду. Для отримання цільової точки ви можете використовувати будь-який механізм, і щоб агент перемістився туди, досить задати поле NavMeshAgent.destination; все інше зробить агент.

У нашому прикладі для переміщення використовуються кліки, тому коли гравець натискає мишею, ми випускаємо промінь з камери в світ у напрямку курсора миші, і якщо він з чимось перетнеться, то ми призначаємо точку зіткнення нової targetPosition агента.

Отже, поєднання цих можливостей надає вам зручний готовий інструмент. Ви можете досить швидко створити просту гру за допомогою функціоналу NavMesh.

# 2. Реалізація характеристик (Діденко В. В.)

Характеристики мають загальний абстрактний клас ```Characteristic```:

```cs
public abstract class Characteristic {
    public enum Type {
        Health,
        Speed,
        Armor,
        Damage
    }
    public float Value { get; set; }
    protected Characteristic(float value) {
        Value = value;
    }
}
```

Існує 4 типи характеристик:
- Здоров'я.
- Шкидкість.
- Бронювання.
- Шкода.

## 2.1 Характеристика здоров’я

Змінна величини здоров’я має вигляд:

```cs
public float HP {
    get => Value;
    private set {
        if (Math.Abs(Value - value) > float.Epsilon) {
          OnHpChange?.Invoke();
        }
        Value = Mathf.Clamp(value, 0, MaxHP);
        if (Value.Equals(0)) {
            OnHealthZero?.Invoke();
        }
    }
}
```
    
Клас ```HealthCharacteristic``` має 2 події:

```cs
private event HealthZero OnHealthZero;
public event HpChange OnHpChange;
```

```OnHealthZero``` визначає подію при якій величина здоров’я стає рівною ```0```
```OnHpChange``` визначає подію при якій величина здоров’я змінює своє значення.

## 2.2 Характеристика швидкості

```cs
public class SpeedCharacteristic : Characteristic {
    public SpeedCharacteristic(float speed) : base(speed) {}
}
```

Характеристика швидкості використовується у сутності ```Person```.

## 2.3 Характеристика бронювання

```cs
public class ArmorCharacteristic : Characteristic {

    private float armorCoefficient;

    public ArmorCharacteristic(float armor, float armorCoefficient = 0.05f) : base(armor) {
        this.armorCoefficient = armorCoefficient;
        }

    public float GetResistance() {
        return 1 / (1 + armorCoefficient * Value);
    }
}
```

Характеристика бронювання використовується для розрахунку кількості шкоди.
Кількість отриманої шкоди залежить від величини опору, що в свою чергу залежить від бронювання.
Величина опору розраховується за формулою:

```cs
public float GetResistance() {
    return 1 / (1 + armorCoefficient * Value);
}
```
, де ```armorCoefficient``` - величина, що визначає наскільки сильно величина опору залежить від величини бронювання;
```Value``` - величина бронювання;
Величина броні є у таких сутностей, як ```Person```, ```ArmorCharacteristic```, а також їх спадкоємців.

## 2.4 Характеристика шкоди

```cs
public class DamageCharacteristic : Characteristic {
    public DamageCharacteristic(float damage) : base(damage){}
}
```

Кількість шкоди розраховується за формулою:

```cs
var damageAccountingArmor = damage * (1 - ((ArmorCharaceristic) Stats[Type.Armor]).GetResistance());
```
, де ```(ArmorCharaceristic) Stats[Type.Armor]``` – клас бронювання, від якого ми отримуємо величину опору;
```damage``` – величина шкоди без урахування опору;

# 3. Клас Item (Діденко В. В.)

```cs
public abstract class Item : ICanBePickedUp {
   public virtual bool TryPick() {
       return Player.Instance.Inventory.TryAdd(this);
   }
}
```

Клас ```Item``` використовується для опису предметів, що можуть бути підняті гравцем.
```Item``` має 2 дочерні класи: ```Scrap``` та ```Equipment```.
```Scrap``` може бути використаним гравцем для підвищення рівня предметів, що можуть бути одягненим гравцем, тобто, спадкоємців класу ```Equipment```.

Спадкоємцями класу ```Equipment``` є:
- ```HeadArmor```.
- ```ChestArmor```.
- ```LegsArmor```.
- ```Weapon```.

# 4. Damageable (Діденко В. В.)

Гравець може бути в двох режимах:
- Людина.
- Робот.

Через велику кількість загальних змінних та методів було вирішено створити загальний клас ```Person```, для якого ```Human``` та ```Robot``` є спадкоємцями.

Клас ```Person```:

```cs
public Dictionary<Type, Characteristic> Stats { get; private set; } = new Dictionary<Type, Characteristic>();

public HealthCharacteristic Health { get; }
public SpeedCharacteristic MoveSpeed { get; protected set; }
public ArmorCharacteristic PersonArmor { get; protected set; }

protected Person(float maxHealth, float moveSpeed, float armor) {
    Health = new HealthCharacteristic(maxHealth, Die);
    MoveSpeed = new SpeedCharacteristic(moveSpeed);
    PersonArmor = new ArmorCharacteristic(armor);
    Stats.Add(Type.Health, Health);
    Stats.Add(Type.Speed, MoveSpeed);
    Stats.Add(Type.Armor, PersonArmor);
}
```

Гравець має ```Dictionary<Type, Characteristic> Stats```, що зберігає загальну кількість характеристик ```Person’а```.

А також ```Health```, ```MoveSpeed```, ```PersonArmor```, що зберігають власні характеристикки ```Person’а```.

Person має 3 спадкоємця:
- ```Human```.
- ```Robot```.
- ```Enemy```.
    
```Enemy``` має дві додаткові змінні, а саме:

```cs
public Transform Transform { get; private set; }
public Animator Animator { get; private set; }
```

Ці змінні необхідні Unity для відображення позиції та анімацій ```Enemy```.

У класі ```Human``` додаткових змінних трохи більше, а саме використовується класс ```Player.UnityData```, який зберігає 4 змінних необхідних Unity.

```cs
public Player.UnityData UnityHumanData { get; private set;}
```

```Player.UnityData``` має наступний вигляд:

```cs
public class UnityData {
    public Transform Transform { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController CharacterController { get; private set; }
    public Transform GunTransform { get; private set; }
            
    public bool Initialized { get; private set; }

    public void Initialize(Transform transform, Animator animator, CharacterController characterController,
        Transform gunTransform) {
        Transform = transform;
        Animator = animator;
        CharacterController = characterController;
        GunTransform = gunTransform;
        Initialized = true;
    }

    public void Uninitialize() {
        Transform = null;
        Animator = null;
        CharacterController = null;
        GunTransform = null;
        Initialized = false;
    }
}
```

Клас Robot має також 4 додаткові змінні запаковані в ```Player.UnityData```, а також поля необхідні для зберігання озброєння.

```cs
public Player.UnityData UnityRobotData { get; private set;}

public readonly Dictionary<Type, EquipmentSlot> equipmentSlots = new Dictionary<Type, EquipmentSlot> {
        {Type.Head, new HeadSlot()},
        {Type.Chest, new ChestSlot()},
        {Type.Legs, new LegsSlot()}
};

public readonly WeaponsSlots weaponsSlots = new WeaponsSlots(new Dictionary<WeaponSlot.Spot, WeaponSlot> {
    {WeaponSlot.Spot.TwoHands, new WeaponSlot(WeaponSlot.Spot.TwoHands)}
});
```

Клас ```Robot``` відрізняється від ```Human``` можливістю надягати озброєння.

Також є клас ```Player```, що зберігає посилання на екземпляри класів ```Human``` та ```Robot```, крім того, містить нинішній стан гравця (робот або людина) та містить посилання на відповідні функції людини чи робота завдяки делегатам:

```cs
public enum State {
    Human,
    Robot
}
```

# 5. Організація роботи зброї (Рихтик Д. М.)

Поведінка та взаємодія зі зброєю реалізована з використанням розподілу логіки по трьом різним класам (скриптам).

## WeaponSO
Цей клас є спадкоємцем ScriptableObject, його зручно використовувати для того, щоб створювати в Unity різні об'єкти, що об'єднані спільними характеристиками та зручно працювати з ними в інтерфейсі Unity-редактора. Також цей клас зберігає внутрішні параметри зброї, такі як шкода за кулю, кількість пострілів у секунду, тощо. Ще цей клас зберігає параметри, які можуть змінюватися по ходу гри, такі як поточна кількість набоїв у магазині.

Крім цього він містить наступні параметри, які змінюються відповідно до конкретного екземпляру зброї:

- ```string name``` - назва зброї в інвентарі.
- ```int weaponID``` - унікальний ідентифікаційний номер зброї.
- ```float damage``` - кількість шкоди, яка завдає одна куля.
- ```float damageSpread``` - максимальний відхил шкоди в обидва боки, ```0``` - шкода завжди буде відповідати значенню damage, ```100``` - якщо значення змінної damage дорівнює ```100```, то підсумкова шкода буде належати інтервалу ```[0, 200]``` та визначатися випадково.
- ```int maxBulletsInMagazine``` - максимальна кількість набоїв у магазині.
- ```int maxAllAmmo``` - максимальна кількість набоїв у інвентарі гравця для даної зброї.
- ```int currentBulletsInMagazine``` - поточна кількість набоїв у магазині.
- ```int allAmmo``` - поточна кількість набоїв у інвентарі гравця для даної зброї.
- ```int bulletsPerShot``` - кількість куль, що вистрілює зброя за один постріл.
- ```float reloadTime``` - час, що витрачається на перезарядку зброї у секундах.
- ```float shotSpread``` - розкид збої при пострілі на 100 метрів.
- ```float shotsPerSecond``` - кількість пострілів у секунду.
- ```float maxShotDistance``` - максимальна відстань ефективного пострілу у метрах.
- ```bool isAutomatic``` - визначає те, чи автоматична зброя (впливає на те, чи можна робити постріли не відтискаючи кнопку пострілу).
- ```AudioClip shotSound``` - звук пострілу.
- ```AudioClip noAmmoSound``` - звук, який програється, коли гравець намагаєтсья зробити постріл, але в магазині немає набоїв.
- ```AudioClip reloadSound``` - звук, який програється при перезарядці зброї.
```bool isReloading``` - визначає те, чи перезаряджається зброя у даний момент.

## Weapon
Цей клас відповідає за внутрішню логіку та поведінку зброї. Містить функції та події які відповідають за постріл, перезарядку, тощо.

```ShotResult``` - ```enum```, що відповідає за відповідь зброї на спробу зробити постріл. Існує декілька варіантів значень цього ```enum'y```, а саме:
- ```NoAmmoInBackpack``` - відсутні набої в інвентарі.
- ```NoAmmoInMagazine``` - відсутні набої у магазині.
- ```ShotSuccesful``` - вистріл виконано успішно.
- ```TooFast``` - пройшло надто мало часу з останнього пострілу.

```TryShoot()``` - метод, який робить спробу пострілу та повертає значення типу ```ShotResult```, після цього клас. ```GunController``` обробляє це значення, та виконує відповідний метод (робить постріл, програє звук перезарядки, тощо).
```Reload()``` - метод, який виконує перезарядку.

## GunController

В цьому класі реалізовані методи, що відповідають за програвання звуків, візуалізацію ефектів та фізичну взаємодію куль та ігрового оточення.
```NoAmmo()``` - метод, що програє звук пострілу коли в магазині немає набоїв.
```Reload()``` - метод, що програє звук перезарядки та викликає метод ```Reload()``` з прив'язаного ```Weapon```.
```Shoot()``` - метод, що програє звук та візуалізує ефекет пострілу, а також завдає шкоду сутностям, з яким взаємодіє куля.
```TryShoot()``` - метод, що викликає метод ```TryShoot()``` з прив'язаного ```Weapon```, і виходячи з поверненого значення викликає вишеперелічені методи.