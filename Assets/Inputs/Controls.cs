// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Combat"",
            ""id"": ""2160702b-579c-4124-a41a-aabc27842725"",
            ""actions"": [
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""64882fc1-8049-4a59-870a-3d8f1db12f96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StartShootingLeft"",
                    ""type"": ""Button"",
                    ""id"": ""79568d5a-6eb6-4064-9eac-293b878f4994"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StopShootingLeft"",
                    ""type"": ""Button"",
                    ""id"": ""69b7972d-5281-4b7c-989c-8b1da07b2c0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StartShootingRight"",
                    ""type"": ""Button"",
                    ""id"": ""2823049e-9951-4d30-a62b-6e5e0a3f2b0c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StopShootingRight"",
                    ""type"": ""Button"",
                    ""id"": ""301676e7-606c-4cb8-a719-531166518ce4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""34cc1274-d46f-4c27-81a6-edf6b1857250"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""StartShootingLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ce2545f-e045-4b6d-9e8f-c683ef025161"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""StopShootingLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9aa8669c-ce61-48c1-b4f6-4e7aec89552f"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68a794b6-43cb-4d6f-923e-265e24cb7b93"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""StopShootingRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc1090ab-d3a0-4f80-bcc0-c2e42430940b"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""StartShootingRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interaction"",
            ""id"": ""1446016e-62cd-4371-bb7e-b5ffa4541676"",
            ""actions"": [
                {
                    ""name"": ""ChangeState"",
                    ""type"": ""Button"",
                    ""id"": ""996b8b89-1ac8-4a3b-9545-d1cbf17823da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ae75f05f-4219-4853-93d1-35ce9df9ae73"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""ChangeState"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Movement"",
            ""id"": ""16dc0843-7e18-4e14-8a04-1d6c3aa1312e"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""b55085b7-784a-4cf4-acba-546aa05fd51a"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""3c97fe28-dcb6-40b0-bd5c-994633b09944"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""90242f95-738a-4d7b-8f3a-5af5efbf4013"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""62dda6a6-fa18-465a-991c-829a12e1454b"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7927a9d9-a774-4399-9847-6a822abeb4e0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""06bed329-cd6c-4753-8309-9660ff72bdc0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4d5f7530-e703-4522-9536-38adb724b352"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""62506b23-8272-40e6-9575-cddcb181c474"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a4177dd8-375d-468f-ac1b-153b59cdad1c"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MainMenu"",
            ""id"": ""8ee613a5-1769-4cc4-8571-ea6b28b27813"",
            ""actions"": [
                {
                    ""name"": ""Clicked"",
                    ""type"": ""Button"",
                    ""id"": ""546f9b15-555a-43b3-a565-1f8e75115a06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""51e7bc5b-0cdf-4341-888e-b5076133f138"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Clicked"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e6f426e-4721-4613-94f9-11ec9fe4b51a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Clicked"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c48c080d-0107-49fb-8bba-1b569f06360d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Clicked"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48ac495b-d801-4c71-a301-a75a5466b09f"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Clicked"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard + Mouse"",
            ""bindingGroup"": ""Keyboard + Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_Reload = m_Combat.FindAction("Reload", throwIfNotFound: true);
        m_Combat_StartShootingLeft = m_Combat.FindAction("StartShootingLeft", throwIfNotFound: true);
        m_Combat_StopShootingLeft = m_Combat.FindAction("StopShootingLeft", throwIfNotFound: true);
        m_Combat_StartShootingRight = m_Combat.FindAction("StartShootingRight", throwIfNotFound: true);
        m_Combat_StopShootingRight = m_Combat.FindAction("StopShootingRight", throwIfNotFound: true);
        // Interaction
        m_Interaction = asset.FindActionMap("Interaction", throwIfNotFound: true);
        m_Interaction_ChangeState = m_Interaction.FindAction("ChangeState", throwIfNotFound: true);
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Movement = m_Movement.FindAction("Movement", throwIfNotFound: true);
        m_Movement_Rotate = m_Movement.FindAction("Rotate", throwIfNotFound: true);
        // MainMenu
        m_MainMenu = asset.FindActionMap("MainMenu", throwIfNotFound: true);
        m_MainMenu_Clicked = m_MainMenu.FindAction("Clicked", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Combat
    private readonly InputActionMap m_Combat;
    private ICombatActions m_CombatActionsCallbackInterface;
    private readonly InputAction m_Combat_Reload;
    private readonly InputAction m_Combat_StartShootingLeft;
    private readonly InputAction m_Combat_StopShootingLeft;
    private readonly InputAction m_Combat_StartShootingRight;
    private readonly InputAction m_Combat_StopShootingRight;
    public struct CombatActions
    {
        private @Controls m_Wrapper;
        public CombatActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Reload => m_Wrapper.m_Combat_Reload;
        public InputAction @StartShootingLeft => m_Wrapper.m_Combat_StartShootingLeft;
        public InputAction @StopShootingLeft => m_Wrapper.m_Combat_StopShootingLeft;
        public InputAction @StartShootingRight => m_Wrapper.m_Combat_StartShootingRight;
        public InputAction @StopShootingRight => m_Wrapper.m_Combat_StopShootingRight;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void SetCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterface != null)
            {
                @Reload.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnReload;
                @StartShootingLeft.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnStartShootingLeft;
                @StartShootingLeft.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnStartShootingLeft;
                @StartShootingLeft.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnStartShootingLeft;
                @StopShootingLeft.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnStopShootingLeft;
                @StopShootingLeft.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnStopShootingLeft;
                @StopShootingLeft.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnStopShootingLeft;
                @StartShootingRight.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnStartShootingRight;
                @StartShootingRight.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnStartShootingRight;
                @StartShootingRight.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnStartShootingRight;
                @StopShootingRight.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnStopShootingRight;
                @StopShootingRight.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnStopShootingRight;
                @StopShootingRight.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnStopShootingRight;
            }
            m_Wrapper.m_CombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @StartShootingLeft.started += instance.OnStartShootingLeft;
                @StartShootingLeft.performed += instance.OnStartShootingLeft;
                @StartShootingLeft.canceled += instance.OnStartShootingLeft;
                @StopShootingLeft.started += instance.OnStopShootingLeft;
                @StopShootingLeft.performed += instance.OnStopShootingLeft;
                @StopShootingLeft.canceled += instance.OnStopShootingLeft;
                @StartShootingRight.started += instance.OnStartShootingRight;
                @StartShootingRight.performed += instance.OnStartShootingRight;
                @StartShootingRight.canceled += instance.OnStartShootingRight;
                @StopShootingRight.started += instance.OnStopShootingRight;
                @StopShootingRight.performed += instance.OnStopShootingRight;
                @StopShootingRight.canceled += instance.OnStopShootingRight;
            }
        }
    }
    public CombatActions @Combat => new CombatActions(this);

    // Interaction
    private readonly InputActionMap m_Interaction;
    private IInteractionActions m_InteractionActionsCallbackInterface;
    private readonly InputAction m_Interaction_ChangeState;
    public struct InteractionActions
    {
        private @Controls m_Wrapper;
        public InteractionActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ChangeState => m_Wrapper.m_Interaction_ChangeState;
        public InputActionMap Get() { return m_Wrapper.m_Interaction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractionActions set) { return set.Get(); }
        public void SetCallbacks(IInteractionActions instance)
        {
            if (m_Wrapper.m_InteractionActionsCallbackInterface != null)
            {
                @ChangeState.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnChangeState;
                @ChangeState.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnChangeState;
                @ChangeState.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnChangeState;
            }
            m_Wrapper.m_InteractionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ChangeState.started += instance.OnChangeState;
                @ChangeState.performed += instance.OnChangeState;
                @ChangeState.canceled += instance.OnChangeState;
            }
        }
    }
    public InteractionActions @Interaction => new InteractionActions(this);

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Movement;
    private readonly InputAction m_Movement_Rotate;
    public struct MovementActions
    {
        private @Controls m_Wrapper;
        public MovementActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Movement_Movement;
        public InputAction @Rotate => m_Wrapper.m_Movement_Rotate;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovement;
                @Rotate.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotate;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // MainMenu
    private readonly InputActionMap m_MainMenu;
    private IMainMenuActions m_MainMenuActionsCallbackInterface;
    private readonly InputAction m_MainMenu_Clicked;
    public struct MainMenuActions
    {
        private @Controls m_Wrapper;
        public MainMenuActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Clicked => m_Wrapper.m_MainMenu_Clicked;
        public InputActionMap Get() { return m_Wrapper.m_MainMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainMenuActions set) { return set.Get(); }
        public void SetCallbacks(IMainMenuActions instance)
        {
            if (m_Wrapper.m_MainMenuActionsCallbackInterface != null)
            {
                @Clicked.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnClicked;
                @Clicked.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnClicked;
                @Clicked.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnClicked;
            }
            m_Wrapper.m_MainMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Clicked.started += instance.OnClicked;
                @Clicked.performed += instance.OnClicked;
                @Clicked.canceled += instance.OnClicked;
            }
        }
    }
    public MainMenuActions @MainMenu => new MainMenuActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard + Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface ICombatActions
    {
        void OnReload(InputAction.CallbackContext context);
        void OnStartShootingLeft(InputAction.CallbackContext context);
        void OnStopShootingLeft(InputAction.CallbackContext context);
        void OnStartShootingRight(InputAction.CallbackContext context);
        void OnStopShootingRight(InputAction.CallbackContext context);
    }
    public interface IInteractionActions
    {
        void OnChangeState(InputAction.CallbackContext context);
    }
    public interface IMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
    }
    public interface IMainMenuActions
    {
        void OnClicked(InputAction.CallbackContext context);
    }
}
