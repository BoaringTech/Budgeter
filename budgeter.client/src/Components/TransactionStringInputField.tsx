interface props {
  label: string;
  property: string | null;
  setProperty: (p: string | null) => void;
}

function TransactionStringInputField({ label, property, setProperty }: props) {
  return (
    <div className="transactionInputField">
      <label className="label">{label}</label>
      <input
        name={label}
        type="text"
        value={property || ""}
        onChange={(e) => setProperty(e.target.value)}
      />
    </div>
  );
}

export default TransactionStringInputField;
