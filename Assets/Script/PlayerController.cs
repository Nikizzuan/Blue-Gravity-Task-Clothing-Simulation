using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;

    // Player attachment animators
    private Animator _BaseAnimator;
    private Animator _HairAnimator;
    private Animator _AccessoriesAnimator;
    private Animator _PantsAnimator;
    private Animator _ClothesAnimator;
    private Animator _ShoesAnimator;
    private Animator _CostumeAnimator;
    public float speed = 5f;


    public GameObject inventoryUI; // Reference to the inventory UI
    public Transform playerItemsParent; // Parent transform for the inventory items
    public GameObject itemPrefab; // Prefab for the inventory item buttons

   

    private bool isInventoryOpen = false;
    

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GameManager.Instance._playerEquipment = GetComponent<PlayerEquipment>();
        if ( GameManager.Instance._playerEquipment == null)
        {
            Debug.LogError("PlayerEquipment component not found on player object.");
            return;
        }

        // Link animators from PlayerEquipment
        _BaseAnimator =  GameManager.Instance._playerEquipment._BaseAnimator;
        _HairAnimator =  GameManager.Instance._playerEquipment._HairAnimator;
        _AccessoriesAnimator =  GameManager.Instance._playerEquipment._AccessoriesAnimator;
        _PantsAnimator =  GameManager.Instance._playerEquipment._PantsAnimator;
        _ClothesAnimator =  GameManager.Instance._playerEquipment._ClothesAnimator;
        _ShoesAnimator =  GameManager.Instance._playerEquipment._ShoesAnimator;
        _CostumeAnimator =  GameManager.Instance._playerEquipment._CostumeAnimator;

        SyncAnimators(0, 0, false); // Initial sync
    }

    void Update()
    {
        // Get input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement vector and apply velocity
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;
        _rb.velocity = movement * speed;

        // Determine if the player is walking
        bool isWalking = movement.x != 0 || movement.y != 0;

        // Update animator states based on movement and sync with other animators
        SyncAnimators(moveHorizontal, moveVertical, isWalking);

        // Open/close inventory with Q key
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleInventory();
        }
    }

    // Method to sync parameters across animators
    void SyncAnimators(float moveX, float moveY, bool isWalking)
    {
        if (isWalking)
        {
            if (_BaseAnimator != null)
            {
                _BaseAnimator.SetFloat("moveX", moveX);
                _BaseAnimator.SetFloat("moveY", moveY);
            }
            if (_HairAnimator != null)
            {
                _HairAnimator.SetFloat("moveX", moveX);
                _HairAnimator.SetFloat("moveY", moveY);
            }
            if (_AccessoriesAnimator != null)
            {
                _AccessoriesAnimator.SetFloat("moveX", moveX);
                _AccessoriesAnimator.SetFloat("moveY", moveY);
            }
            if (_PantsAnimator != null)
            {
                _PantsAnimator.SetFloat("moveX", moveX);
                _PantsAnimator.SetFloat("moveY", moveY);
            }
            if (_ClothesAnimator != null)
            {
                _ClothesAnimator.SetFloat("moveX", moveX);
                _ClothesAnimator.SetFloat("moveY", moveY);
            }
            if (_ShoesAnimator != null)
            {
                _ShoesAnimator.SetFloat("moveX", moveX);
                _ShoesAnimator.SetFloat("moveY", moveY);
            }
            if (_CostumeAnimator != null)
            {
                _CostumeAnimator.SetFloat("moveX", moveX);
                _CostumeAnimator.SetFloat("moveY", moveY);
            }
        }
        if (_BaseAnimator != null)
            _BaseAnimator.SetBool("isWalking", isWalking);
        if (_HairAnimator != null)
            _HairAnimator.SetBool("isWalking", isWalking);
        if (_AccessoriesAnimator != null)
            _AccessoriesAnimator.SetBool("isWalking", isWalking);
        if (_PantsAnimator != null)
            _PantsAnimator.SetBool("isWalking", isWalking);
        if (_ClothesAnimator != null)
            _ClothesAnimator.SetBool("isWalking", isWalking);
        if (_ShoesAnimator != null)
            _ShoesAnimator.SetBool("isWalking", isWalking);
        if (_CostumeAnimator != null)
            _CostumeAnimator.SetBool("isWalking", isWalking);
    }

    // Equip clothing item
    public void EquipClothingItem(ClothingItem item)
    {
         GameManager.Instance._playerEquipment.EquipItem(item);
        // Link animators from PlayerEquipment again to ensure they are up to date
        _BaseAnimator =  GameManager.Instance._playerEquipment._BaseAnimator;
        _HairAnimator =  GameManager.Instance._playerEquipment._HairAnimator;
        _AccessoriesAnimator =  GameManager.Instance._playerEquipment._AccessoriesAnimator;
        _PantsAnimator =  GameManager.Instance._playerEquipment._PantsAnimator;
        _ClothesAnimator =  GameManager.Instance._playerEquipment._ClothesAnimator;
        _ShoesAnimator =  GameManager.Instance._playerEquipment._ShoesAnimator;
        _CostumeAnimator =  GameManager.Instance._playerEquipment._CostumeAnimator;
        SyncAnimators(0, 0, false); // Re-sync animators after equipping new item
    }

    // Expose IsItemEquipped method
    public bool IsItemEquipped(ClothingItem item)
    {
        return  GameManager.Instance._playerEquipment.IsItemEquipped(item);
    }

    // Toggle inventory UI
    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);
        if (isInventoryOpen)
        {
            PopulatePlayerItems();
        }
    }

    // Populate inventory UI with player's items
    private void PopulatePlayerItems()
    {
        foreach (Transform child in playerItemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (ClothingItem item in InventoryManager.Instance.GetClothingItems())
        {
            GameObject itemGO = Instantiate(itemPrefab, playerItemsParent);
            itemGO.GetComponent<ItemButton>().Setup(item, GameManager.Instance.shopkeeper, InventoryManager.Instance, this);
        }
    }
}
