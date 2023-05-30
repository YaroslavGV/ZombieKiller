using System;
using Defective.JSON;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/StackableItem")]
    public class StackableItem : Item
    {
        private const string AmountParameter = "amount";
        [SerializeField] private int _amount = 1;

        public virtual int Amount 
        {
            get => _amount;
            set
            {
                if (value < 0)
                    throw new Exception("Amount cannot be negative");
                if (_amount == value)
                    return;
                _amount = value;
                Change();
                if (_amount == 0)
                    Consume();
            }
        }

        public override string GetParameters ()
        {
            JSONObject jObject = new JSONObject();
            jObject.AddField(AmountParameter, _amount);
            return jObject.ToString();
        }

        public override void SetParameters (string json)
        {
            if (string.IsNullOrEmpty(json))
                return;

            JSONObject jObject = new JSONObject(json);
            if (jObject.HasField(AmountParameter))
            {
                JSONObject jParam = jObject.GetField(AmountParameter);
                if (jParam.isInteger)
                    _amount = jParam.intValue;
            }
        }
    }
}