interface props {
  label: string;
  property: boolean;
  setProperty: (p: boolean) => void;
}

function TransactionButtonInputField({ label, property, setProperty }: props) {
  return (
    <div className="transactionInputField">
      <label className="label">{label}</label>
      <input
        name={label}
        type="checkbox"
        step="0.01"
        min="0"
        placeholder="0.00"
        checked={property}
        onChange={(e) => setProperty(e.target.checked)}
      />
    </div>
  );
}

export default TransactionButtonInputField;
