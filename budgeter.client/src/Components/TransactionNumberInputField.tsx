interface props {
  label: string;
  property: number;
  setProperty: (p: number) => void;
}

function TransactionNumberInputField({ label, property, setProperty }: props) {
  const displayValue = property.toFixed(2);

  const onChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const rawValue = e.target.value;

    if (rawValue === "") {
      setProperty(0);
      return;
    }

    const numValue = limitTwoDecimalPlaces(rawValue);
    setProperty(numValue);
  };

  const onBlur = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = parseFloat(e.target.value);

    if (isNaN(value)) {
      setProperty(0);
    } else {
      setProperty(Math.round(value * 100) / 100);
    }
  };

  return (
    <div className="transactionInputField">
      <label className="label">{label}</label>
      <input
        name={label}
        type="number"
        step="0.01"
        min="0"
        placeholder="0.00"
        value={displayValue}
        onChange={onChange}
        onBlur={onBlur}
      />
    </div>
  );
}

function limitTwoDecimalPlaces(input: string): number {
  if (!input) return 0.0;

  let value = input.replace(/[^0-9.]/g, ""); // Clean up input

  const parts = value.split(".");
  // Ensure only one decimal point
  if (parts.length > 2) {
    value = parts[0] + "." + parts.slice(1).join("");
  }

  // Format to 2 decimals
  if (parts[1] && parts[1].length > 2) {
    value = parts[0] + "." + parts[1].slice(1);
  }

  // Parse to number
  const numValue = parseFloat(value);
  if (isNaN(numValue)) return 0.0;

  return numValue;
}

export default TransactionNumberInputField;
