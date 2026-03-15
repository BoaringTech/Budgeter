interface props {
  saving: boolean;
  onSave: () => void;
  onCancel: () => void;
}

function TransactionSaveButtons({ saving, onSave, onCancel }: props) {
  return (
    <div className="transactionSelectionButtons">
      <button type="button" onClick={onSave}>
        {!saving ? "Save" : "Saving..."}
      </button>
      <button onClick={onCancel}>Cancel</button>
    </div>
  );
}

export default TransactionSaveButtons;
