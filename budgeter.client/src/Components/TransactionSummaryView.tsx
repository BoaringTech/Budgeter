interface props {
  category: string | null;
  amount: number | null;
  merchant: string | null;
  note: string | null;
}

function TransactionSummaryView({ category, amount, merchant, note }: props) {
  return (
    <>
      <td>
        <p>{category}</p>
      </td>
      <td>
        <h3>{merchant}</h3>
      </td>
      <td>
        <p>{note}</p>
      </td>
      <td>
        <p>{amount}</p>
      </td>
    </>
  );
}

export default TransactionSummaryView;
