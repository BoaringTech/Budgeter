interface props {
  category: string | null;
  amount: number | null;
  merchant: string | null;
  note: string | null;
}

function TransactionSummaryView({ category, amount, merchant, note }: props) {
  return (
    <>
      <p className="category">{category}</p>
      <h3 className="merchant">{merchant}</h3>
      <p className="note">{note}</p>
      <p className="amount">{amount}</p>
    </>
  );
}

export default TransactionSummaryView;
