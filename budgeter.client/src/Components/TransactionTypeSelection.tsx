import type { TransactionTypes } from "../Enums/TransactionTypes";

interface props {
  selectedTransactionType: TransactionTypes;
  setTransactionType: (t: TransactionTypes) => void;
}

function TransactionTypeSelection({
  selectedTransactionType,
  setTransactionType,
}: props) {
  return (
    <div className="transactionSelectionButtons">
      <button
        type="button"
        disabled={selectedTransactionType === "Income"}
        onClick={() => setTransactionType("Income")}
      >
        Income
      </button>
      <button
        type="button"
        disabled={selectedTransactionType === "Expense"}
        onClick={() => setTransactionType("Expense")}
      >
        Expense
      </button>
      <button
        type="button"
        disabled={selectedTransactionType === "Transfer"}
        onClick={() => setTransactionType("Transfer")}
      >
        Transfer
      </button>
    </div>
  );
}

export default TransactionTypeSelection;
