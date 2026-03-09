interface props {
  category: string | null;
  amount: number | null;
  merchant: string | null;
  note: string | null;
}

function TransactionSummaryView({ category, amount, merchant, note }: props) {
  return (
    <span>
      <p>{category}</p>
      <div>
        <h3>{merchant}</h3>
        <p>{note}</p>
      </div>
      <p>{amount}</p>
    </span>
  );
}

export default TransactionSummaryView;
