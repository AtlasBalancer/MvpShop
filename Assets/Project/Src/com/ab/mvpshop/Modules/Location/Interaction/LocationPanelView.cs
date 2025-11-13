using TMPro;
using UnityEngine.UI;
using com.ab.mvpshop.core.mvp;
using UnityEngine;

namespace com.ab.mvpshop.modules.location
{
    public class LocationPanelView : BaseView
    {
        public Button ChangeButton;
        public TMP_Text CurrentLocation;
        public AdvancedDropdown DropDown;
        public Animator ChangeButtonAnimator;
        
        const string NORMAL_ANIMATION = "Normal";

        public void ActiveChoseLocation(bool active) =>
            DropDown.transform.localScale = active ? Vector3.one : Vector3.zero;

        public void ActiveDropDown()
        {
            ChangeButton.interactable = false;
            ChangeButtonAnimator.SetTrigger(NORMAL_ANIMATION);

            ActiveChoseLocation(true);
            DropDown.OpenOptions();
        }

        public void ActiveButton(int _)
        {
            ChangeButton.interactable = true;

            ActiveChoseLocation(false);
            DropDown.CloseOptions();
        }

        public void SetUp()
        {
            ActiveChoseLocation(false);
            ChangeButton.onClick.AddListener(ActiveDropDown);
            DropDown.onChangedValue += ActiveButton;
        }

        public void UpdateLocation(string location) =>
            CurrentLocation.SetText(location);

        void OnDestroy()
        {
            ChangeButton.onClick.RemoveListener(ActiveDropDown);
            DropDown.onChangedValue -= ActiveButton;
        }
    }
}