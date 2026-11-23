# C# Style Guide
This document is a style guide for C# code to be used in Unity projects. The rules described here must prevail.

## Naming
For naming rules follow the Microsoft C# naming guidelines.

### Code
* Names of classes, methods, enumerations, public fields, public properties, namespaces: **PascalCase**.
* Names of local variables, parameters: **camelCase**.
* Names of private, protected, internal and protected internal fields and properties: **_camelCase**.
* Naming convention is unaffected by modifiers such as const, static, readonly, etc.
* Use casing for anything that is not a **word (anything written without internal spaces)**, including acronyms like NPC. For example, use **NpcManager** instead of **NPCManager**.
* Names of interfaces start with I. For example, IInterface.
* Names of booleans usually start with a prefix that functions as an auxiliary verb, such as ``is`` to express a state or condition (isReady, isInteractable), ``has`` to indicate possession or availability (hasErrors, hasPermission), ``can`` to denote ability or permission (canExecute, canEdit) and ``should`` to suggest recommendation or expectation (shouldRetry, shouldNotify).

### Files
* Names of files must follow the rules described in [Folders and Files Naming](file-naming.md).
* Where possible the file name should be the same as the name of the main class in the file. If you have a **MonoBehaviour** in a file, the source file name must match and should only exist one per file.
* In general, prefer one core class per file.

## Organization
In this section we have rules of how to organize scripts files.

* Using declarations go at the top, before any namespaces.
* Right after using declarations, use namespaces to prevent conflicts between classes, interfaces, enums, etc.
* Class member ordering:
  - Group class members in the following order:
    - Nested classes and enums
    - Nested enumerations
    - Inspector field or properties (Here you can follow your heart and *mix and match* fields and properties the way you think will be the best for displaying in the Inspector)
    - Fields
    - Properties
    - Events and Delegates
    - MonoBehaviour methods (Order this methods following the Unity order of execution).
    - Methods.
  - Within each group, elements should be in the following order:
    - Public.
    - Internal.
    - Protected internal.
    - Protected.
    - Private.

## Formatting
Here is presented some formatting rules:

* Use Allman style braces.
* Always use braces following the Allman Style.
* Use four spaces of indentation (no tabs).
* Line width of 120 characters max. To break a long line of code, add a indentation level on the breaked part.
* Attributes on the line above the field, property, or method. Single attribute per line.

## Example
``` cs
// USING LINES:
// - Keep using lines at the top of your file, outside the namespace.
// - Remove unused lines.
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// NAMESPACES:
// - PascalCase, without special symbols or underscores.
// - Create sub-namespaces with the dot (.) operator.
namespace ProjectGuide.CodeExample  
{
    // ENUMERATIONS:
    // - PascalCase.
    // - Singular type name.
    public enum Direction
    {
        North,
        South,
        East,
        West,
    }

    // FLAGS ENUMERATIONS:
    // - PascalCase.
    // - Plural type name 
    // - Use column-alignment for binary values
    [Flags]
    public enum AttackModes
    {
        // Decimal                         // Binary
        None = 0,                          // 000000
        Melee = 1,                         // 000001
        Ranged = 2,                        // 000010
        Special = 4,                       // 000100

        MeleeAndSpecial = Melee | Special  // 000101
    }

    // INTERFACES:
    // - PascalCase.
    // - Name interfaces with adjective phrases.
    // - Use the 'I' prefix.
    public interface IDamageable
    {
        public void ApplyDamage(float damage);
    }

    // CLASSES:
    // - PascalCase.
    // - Name them with nouns or noun phrases.
    // - One MonoBehaviour per file. If you have a MonoBehaviour in a file, the source file name must match. 
    public class StyleExample : MonoBehaviour, IDamageable
    {
        // NESTED CLASSES AND ENUMERATIONS:
        // - If you have any, group and keep them at the start of the class.
        [Serializable]
        private class CharacterStats
        {
            public float JumpForce = 10;
            public float MoveSpeed = 50;
            public float RotationSpeed = 10;
            public float MaxHealth = 100;
        }

        // INSPECTOR FIELDS AND PROPERTIES
        // - Group fields and properties that you need to show in the inspector here, after  
        // nested classes and enumerations and before normal fields
        [field: Header("Character Profile")]
        [field: SerializeField]
        public string Name { get; private set; }

        [field: TextArea]
        [field: SerializeField]
        public string History { get; private set; }

        [Space]
        [SerializeField]
        private CharacterStats _baseStats;

        [Header("Inputs")]
        [SerializeField] private InputAction _moveAction;
        [SerializeField] private InputAction _jumpAction;

        // FIELDS:
        // - Avoid special characters (backslashes, symbols, Unicode characters).
        // - Use nouns for names, but prefix booleans with a verb.
        // - Use meaningful names. Make names searchable and pronounceable. Don’t abbreviate (unless it’s math).
        // - Specify the default access modifier.
        // - Field initializers are encouraged.
        // - Group fields that will not show in the inspector here, between inspector fields/properties and
        // properties that will also not be show in the inspector.
        private Rigidbody _rigidBody;

        // PROPERTIES:
        // - Use expression-bodied for read-only properties but { get; set; } for everything else.
        // - Prefer use the Auto-Implementated Property for a public property without a backing field.
        // - Use meaningful names. Make names searchable and pronounceable.
        // - Group fields that will not show in the inspector here, between fields and
        // properties that will also not be show in the inspector.
        public float Health { get; private set; }

        // EVENTS:
        // - Name with a verb phrase.
        // - Present participle means "before" and past participle mean "after."
        // - Use System.Action delegate for most events (can take 0 to 16 parameters).
        // - To display an event in inspector you can use UnityEvent
        public Action CharacterDying;

        // MONOBEHAVIOUR METHODS
        // - Group MonoBehaviour methods here, before any other method.
        // - Order them following the Unity order of execution.
        // - Remove unused methods, like Start and Update that is usually auto created when you
        // create a MonoBehaviour script in Unity;
        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();

            _moveAction.Enable();
            _jumpAction.Enable();
        }

        private void Update()
        {
            Move();
            Jump();
        }

        // METHODS:
        // - PascalCase.
        // - Start a methods name with a verbs or verb phrases to show an action.
        // - Parameter names are camelCase with single space after comma.
        // - No spaces: between the method name and parenthesis and in between method arguments and parenthesis.
        // - Order method by: Public -> Internal -> Protected Internal -> Protected -> Private.
        public void SetInitialPosition(float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }

        public void ApplyDamage(float damage)
        {
            Health = Mathf.Clamp(Health - damage, 0.0f, _baseStats.MaxHealth);

            if (Health == 0.0f)
            {
                CharacterDying();
            }
        }

        // Methods ask a question when they return bool.
        public bool IsCharacterDead()
        {
            return Health == 0.0f;
        }

        private void Move()
        {
            if (!_moveAction.IsPressed())
            {
                return;
            }

            Vector2 movement = _moveAction.ReadValue<Vector2>();

            Vector3 characterRotation = transform.eulerAngles;
            characterRotation.y += movement.x * _baseStats.RotationSpeed * Time.deltaTime;
            transform.eulerAngles = characterRotation;

            Vector3 movementForce = _baseStats.MoveSpeed * movement.y * Time.deltaTime * transform.forward;
            _rigidBody.AddForce(movementForce, ForceMode.Force);
        }

        private void Jump()
        {
            if (!_jumpAction.WasPerformedThisFrame())
            {
                return;
            }

            _rigidBody.AddForce(new Vector3(0, _baseStats.JumpForce, 0), ForceMode.Impulse);
        }

        private void FormatExamples(int someExpression)
        {
            // VAR:
            // - Use var if it helps readability, especially with long type names.
            // - Avoid var if it makes the type ambiguous.
            var powerUps = new List<CharacterStats>();
            var dict = new Dictionary<string, List<GameObject>>();

            // SWITCH STATEMENTS:
            // - Indent each case and the break underneath.
            switch (someExpression)
            {
                case 0:
                    // ..
                    break;
                case 1:
                    // ..
                    break;
                case 2:
                    // ..
                    break;
            }

            // BRACES: 
            // - Always use braces following the Allman Style.
            for (int i = 0; i < 100; i++)
            {
                // ..
            }

            // Don't remove the braces here.
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    // ..
                }
            }
        }
    }
}
```
