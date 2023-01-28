using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [Header("Parameters")]
    public UIInteractionController m_uiInteractionController;
    public PlayerViewController m_viewController;
    public PlayerInventory m_inventory;
    public float m_maxInteractDistance = 1.0f;

    // Delegates and events
    public delegate void InteractionEventHandler();
    public delegate void InteractableEventHandler(InteractableItem interactable);
    public delegate void ShortTextEventHandler(ShortTextItem inspectable);
    public delegate void InspectableEventHandler(InspectableItem inspectable);
    public delegate void CollectableEventHandler(CollectableItem inspectable);

    public InteractableEventHandler m_eventInteractableFound;
    public ShortTextEventHandler m_eventShortTextFound;
    public InspectableEventHandler m_eventInspectableFound;
    public CollectableEventHandler m_eventCollectableFound;
    public InteractionEventHandler m_eventInteractableLost;

    public InteractableEventHandler m_eventInteract;
    public InspectableEventHandler m_eventInteractWithInspectable;
    public CollectableEventHandler m_eventInteractWithCollectable;

    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        if (m_uiInteractionController == null)
        {
            Debug.LogError("No UIInteractionController found!");
        }
        else
        {
            // Interactable
            m_eventInteractableFound += m_uiInteractionController.EnableInteractText;
            m_eventInteractableLost += m_uiInteractionController.DisableInteractText;

            // ShortText
            m_eventShortTextFound += (ShortTextItem item) => { m_uiInteractionController.ShowShortText(item.m_shortText); };
            m_eventInteractableLost += m_uiInteractionController.DisableShortText;

            // Inspectable
            m_eventInteractWithInspectable += m_uiInteractionController.OnInteractWithInspectable;


            m_uiInteractionController.m_uiMenuInspectionController.m_eventOnInspectionEnd += CheckInventoryToEndGame;
        }

        if (m_viewController == null)
        {
            Debug.LogError("No PlayerViewController found!");
        }

        if (m_inventory == null)
        {
            Debug.LogError("No PlayerInventory found!");
        }
        else
        {
            m_eventInteractWithCollectable += m_inventory.AddToInventory;
        }
        m_animator = gameObject.GetComponent<Animator>();
        if (!m_animator)
            Debug.LogError("Animator not found");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(
            m_viewController.CharactorCamera.transform.position,
            m_viewController.CharactorCamera.transform.forward,
            out hit,
            m_maxInteractDistance,
            LayerMask.GetMask("Interactable")
            )) // If found interactable object
        {
            GameObject goInteractable = hit.collider.gameObject;
            InteractableItem interactable = goInteractable.GetComponent<InteractableItem>();
            if (interactable == null)
            {
                Debug.LogError("InteractableItem cannot be found on interactable object [" + goInteractable.name + "]!");
            }
            else
            {
                m_eventInteractableFound?.Invoke(interactable);
                FindInteractiable(interactable);

                if (Input.GetButtonDown("Interact"))
                {
                    m_eventInteract?.Invoke(interactable);
                    InteractWithInteractable(interactable);
                }
            }
        }
        else
        {
            m_eventInteractableLost?.Invoke();
        }
    }

    void FindInteractiable(InteractableItem interactable)
    {
        // ShortText
        if (interactable.m_shortTextItem)
        {
            m_eventShortTextFound?.Invoke(interactable.m_shortTextItem);
        }

        // Inspectable
        if (interactable.m_inspectableItem)
        {
            m_eventInspectableFound?.Invoke(interactable.m_inspectableItem);
        }

        // Collectable
        if (interactable.m_collectableItem)
        {
            m_eventCollectableFound?.Invoke(interactable.m_collectableItem);
        }
    }

    void InteractWithInteractable(InteractableItem interactable)
    {
        // Inspectable
        if (interactable.m_inspectableItem)
        {
            m_eventInteractWithInspectable?.Invoke(interactable.m_inspectableItem);
        }

        // Collectable
        if (interactable.m_collectableItem)
        {
            m_animator.SetTrigger("Grab");
            m_eventInteractWithCollectable?.Invoke(interactable.m_collectableItem);
        }

        // Triggerable
        if (interactable.m_triggerableItem)
        {
            interactable.m_triggerableItem.Trigger(m_inventory.Items);
        }
    }

    public void CheckInventoryToEndGame()
    {
        if(m_inventory.CheckInventoryFull())
        {
            GameplayManager.Instance.LoadEndScene();
        }
    }
}
