using UnityEngine;

namespace Project.Scripts.UserInterface
{
    public class CursorController : MonoBehaviour
    {
        public enum CursorType
        {
            Default,
            Scope
        }

        #region Properties

        [Header("Cursors")]
        [SerializeField] private Texture2D defaultCursor;
        [SerializeField] private Texture2D scopeCursor;

        [Header("Settings")]
        [SerializeField] private CursorType cursorByDefault = CursorType.Default;

        #endregion

        private void Start()
        {
            SetCursor(cursorByDefault);
        }

        public void SetCursor(CursorType cursorType)
        {
            Texture2D cursor = null;
            switch(cursorType)
            {
                case CursorType.Default:
                    cursor = defaultCursor;
                    break;
                case CursorType.Scope:
                    cursor = scopeCursor;
                    break;
            }

            Cursor.SetCursor(cursor, new Vector2(32, 32), CursorMode.Auto);
        }
    }
}
