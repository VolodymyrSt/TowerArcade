using DI;
using Sound;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class BaseInventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler
    {
        private SoundHandler _menuSoundHandler;

        private void Awake() => 
            _menuSoundHandler = MenuDI.Resolve<SoundHandler>();

        public virtual void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;

            var item = eventData.pointerDrag.GetComponent<InventoryItem>();

            if (item == null) return;

            if (item.IsInMainSlot())
            {
                return;
            }

            if (transform.childCount == 0)
            {
                item.SetParentAfterDrag(transform);
            }
            else
            {
                var existingItem = transform.GetChild(0).GetComponent<InventoryItem>();

                if (existingItem == null) return;

                if (existingItem.IsInMainSlot())
                {
                    item.SetToMain(true);
                    existingItem.SetToMain(false);

                    existingItem.SetParentAfterDrag(item.GetOriginalParent());

                    item.SetParentAfterDrag(transform);
                }
                else
                {
                    existingItem.ReplaceItem(item);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData) => 
            _menuSoundHandler.PlaySound(ClipName.Selected);
    }
}
