interface props {
  category: string | null;
  amount: number | null;
  merchant: string | null;
  notes: string | null;
}

function TransactionSummaryView({ category, amount, merchant, notes }: props) {
  return (
    <>
      <p className="category">{category || "Uncategorized"}</p>
      <div className="middle-column">
        <p className="merchant">{merchant}</p>
        <p className="note">{notes}</p>
      </div>
      <p className="amount">{formatAmount(amount || 0)}</p>
    </>
  );
}

function formatAmount(amount: number): string {
  return amount.toFixed(2);
}

export default TransactionSummaryView;
