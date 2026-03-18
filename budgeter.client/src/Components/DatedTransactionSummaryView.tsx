import "../StyleSheets/TransactionSummary.css";

interface props {
  date: Date;
  category: string | null;
  amount: number | null;
  merchant: string | null;
  notes: string | null;
}

function DatedTransactionSummaryView({
  date,
  category,
  amount,
  merchant,
  notes,
}: props) {
  const getDateString = (date: Date | null): string => {
    if (!date) {
      const now = new Date();
      const year = now.getFullYear();
      const month = String(now.getMonth() + 1).padStart(2, "0");
      const day = String(now.getDate()).padStart(2, "0");
      return `${year}-${month}-${day}`;
    }

    const dateObj = date instanceof Date ? date : new Date(date);
    const year = dateObj.getFullYear();
    const month = String(dateObj.getMonth() + 1).padStart(2, "0");
    const day = String(dateObj.getDate()).padStart(2, "0");
    return `${year}-${month}-${day}`;
  };

  return (
    <>
      <div className="left-column">
        <p>{getDateString(date)}</p>
        <p className="category">{category || "Uncategorized"}</p>
      </div>
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

export default DatedTransactionSummaryView;
