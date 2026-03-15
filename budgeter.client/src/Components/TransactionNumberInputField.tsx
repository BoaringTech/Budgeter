interface props {
  label: string;
  property: number;
  setProperty: (p: number) => void;
}

function TransactionNumberInputField({ label, property, setProperty }: props) {
  return (
    <div className="transactionInputField">
      <label className="label">{label}</label>
      <input
        name={label}
        type="number"
        step="0.01"
        min="0"
        placeholder="0.00"
        value={property}
        onChange={(e) =>
          setProperty(limitTwoDecimalPlaces(e.target.value) || 0)
        }
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
