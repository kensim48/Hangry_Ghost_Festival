// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/ControllerScheme.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ControllerScheme : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControllerScheme()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControllerScheme"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""c8952581-94b7-4ae3-9278-a4b176b71957"",
            ""actions"": [
                {
                    ""name"": ""LeftArmMovement"",
                    ""type"": ""Button"",
                    ""id"": ""34f7cb4b-4591-4b7e-a6c5-ac22ec0eaf03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightArmMovement"",
                    ""type"": ""Button"",
                    ""id"": ""c4d23a47-5b20-45da-bf94-e4738f90e771"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftTrigger"",
                    ""type"": ""Button"",
                    ""id"": ""67d2dc80-6132-4754-a12a-e7cb7f4b880f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightTrigger"",
                    ""type"": ""Button"",
                    ""id"": ""e876a7de-c34a-478d-8930-4b34c3fbfaec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftShoulder"",
                    ""type"": ""Button"",
                    ""id"": ""0eeffd64-8296-4b00-89bb-0562720d6e13"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightShoulder"",
                    ""type"": ""Button"",
                    ""id"": ""de064034-bbff-40ad-a87d-6478c3e7047c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightPrime"",
                    ""type"": ""Button"",
                    ""id"": ""c9603813-ddf2-4f14-b0b9-28c83256705f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftPrime"",
                    ""type"": ""Button"",
                    ""id"": ""3d862ec6-5093-4475-be1a-7154ca57e39b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""18696f7b-5075-4d20-b72f-c60429f3b084"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftArmMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7701b0f6-9403-47bc-83e9-393c12a11ca0"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f67edb44-30fc-4c0c-8682-1c746832a182"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b124eaf-9abb-49aa-a56a-687507b3e8f7"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightArmMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d09110b-2193-4c7e-9abf-2a33e12ce25c"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftShoulder"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d98a3559-c57a-4a95-b80c-55915494e160"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightShoulder"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7e78129-2520-4bd0-83dd-ea658659c354"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightPrime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f3fbe1f-619a-4e8c-bfe4-20661c53a6e3"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftPrime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_LeftArmMovement = m_Gameplay.FindAction("LeftArmMovement", throwIfNotFound: true);
        m_Gameplay_RightArmMovement = m_Gameplay.FindAction("RightArmMovement", throwIfNotFound: true);
        m_Gameplay_LeftTrigger = m_Gameplay.FindAction("LeftTrigger", throwIfNotFound: true);
        m_Gameplay_RightTrigger = m_Gameplay.FindAction("RightTrigger", throwIfNotFound: true);
        m_Gameplay_LeftShoulder = m_Gameplay.FindAction("LeftShoulder", throwIfNotFound: true);
        m_Gameplay_RightShoulder = m_Gameplay.FindAction("RightShoulder", throwIfNotFound: true);
        m_Gameplay_RightPrime = m_Gameplay.FindAction("RightPrime", throwIfNotFound: true);
        m_Gameplay_LeftPrime = m_Gameplay.FindAction("LeftPrime", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_LeftArmMovement;
    private readonly InputAction m_Gameplay_RightArmMovement;
    private readonly InputAction m_Gameplay_LeftTrigger;
    private readonly InputAction m_Gameplay_RightTrigger;
    private readonly InputAction m_Gameplay_LeftShoulder;
    private readonly InputAction m_Gameplay_RightShoulder;
    private readonly InputAction m_Gameplay_RightPrime;
    private readonly InputAction m_Gameplay_LeftPrime;
    public struct GameplayActions
    {
        private @ControllerScheme m_Wrapper;
        public GameplayActions(@ControllerScheme wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftArmMovement => m_Wrapper.m_Gameplay_LeftArmMovement;
        public InputAction @RightArmMovement => m_Wrapper.m_Gameplay_RightArmMovement;
        public InputAction @LeftTrigger => m_Wrapper.m_Gameplay_LeftTrigger;
        public InputAction @RightTrigger => m_Wrapper.m_Gameplay_RightTrigger;
        public InputAction @LeftShoulder => m_Wrapper.m_Gameplay_LeftShoulder;
        public InputAction @RightShoulder => m_Wrapper.m_Gameplay_RightShoulder;
        public InputAction @RightPrime => m_Wrapper.m_Gameplay_RightPrime;
        public InputAction @LeftPrime => m_Wrapper.m_Gameplay_LeftPrime;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @LeftArmMovement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftArmMovement;
                @LeftArmMovement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftArmMovement;
                @LeftArmMovement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftArmMovement;
                @RightArmMovement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightArmMovement;
                @RightArmMovement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightArmMovement;
                @RightArmMovement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightArmMovement;
                @LeftTrigger.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftTrigger;
                @LeftTrigger.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftTrigger;
                @LeftTrigger.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftTrigger;
                @RightTrigger.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightTrigger;
                @RightTrigger.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightTrigger;
                @RightTrigger.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightTrigger;
                @LeftShoulder.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftShoulder;
                @LeftShoulder.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftShoulder;
                @LeftShoulder.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftShoulder;
                @RightShoulder.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightShoulder;
                @RightShoulder.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightShoulder;
                @RightShoulder.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightShoulder;
                @RightPrime.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightPrime;
                @RightPrime.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightPrime;
                @RightPrime.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightPrime;
                @LeftPrime.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftPrime;
                @LeftPrime.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftPrime;
                @LeftPrime.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftPrime;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftArmMovement.started += instance.OnLeftArmMovement;
                @LeftArmMovement.performed += instance.OnLeftArmMovement;
                @LeftArmMovement.canceled += instance.OnLeftArmMovement;
                @RightArmMovement.started += instance.OnRightArmMovement;
                @RightArmMovement.performed += instance.OnRightArmMovement;
                @RightArmMovement.canceled += instance.OnRightArmMovement;
                @LeftTrigger.started += instance.OnLeftTrigger;
                @LeftTrigger.performed += instance.OnLeftTrigger;
                @LeftTrigger.canceled += instance.OnLeftTrigger;
                @RightTrigger.started += instance.OnRightTrigger;
                @RightTrigger.performed += instance.OnRightTrigger;
                @RightTrigger.canceled += instance.OnRightTrigger;
                @LeftShoulder.started += instance.OnLeftShoulder;
                @LeftShoulder.performed += instance.OnLeftShoulder;
                @LeftShoulder.canceled += instance.OnLeftShoulder;
                @RightShoulder.started += instance.OnRightShoulder;
                @RightShoulder.performed += instance.OnRightShoulder;
                @RightShoulder.canceled += instance.OnRightShoulder;
                @RightPrime.started += instance.OnRightPrime;
                @RightPrime.performed += instance.OnRightPrime;
                @RightPrime.canceled += instance.OnRightPrime;
                @LeftPrime.started += instance.OnLeftPrime;
                @LeftPrime.performed += instance.OnLeftPrime;
                @LeftPrime.canceled += instance.OnLeftPrime;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnLeftArmMovement(InputAction.CallbackContext context);
        void OnRightArmMovement(InputAction.CallbackContext context);
        void OnLeftTrigger(InputAction.CallbackContext context);
        void OnRightTrigger(InputAction.CallbackContext context);
        void OnLeftShoulder(InputAction.CallbackContext context);
        void OnRightShoulder(InputAction.CallbackContext context);
        void OnRightPrime(InputAction.CallbackContext context);
        void OnLeftPrime(InputAction.CallbackContext context);
    }
}
