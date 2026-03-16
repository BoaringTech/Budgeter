interface props {
  saving: boolean;
  deleting: boolean;
  deletable: boolean;
  onSave: () => void;
  onDelete: () => void;
  onCancel: () => void;
}

function TransactionSaveButtons({
  saving,
  deleting,
  deletable,
  onSave,
  onDelete,
  onCancel,
}: props) {
  return (
    <div className="transactionSelectionButtons">
      <button type="button" onClick={onSave}>
        {!saving ? "Save" : "Saving..."}
      </button>
      {deletable && (
        <button type="button" onClick={onDelete}>
          {!deleting ? "Delete" : "Deleting..."}
        </button>
      )}
      <button onClick={onCancel}>Cancel</button>
    </div>
  );
}

export default TransactionSaveButtons;
