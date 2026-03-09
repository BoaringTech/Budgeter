import { useState, useEffect } from "react";
import type { Transaction } from "../Interfaces/Transaction";
import TransactionSummaryView from "./TransactionSummaryView";

interface props {
  setSelectedTransactionId: (transaction: number | null) => void;
}

function DailyTransactionListView({ setSelectedTransactionId }: props) {
  const [transactions, setTransactions] = useState<Transaction[]>([]);

  useEffect(() => {
    // Fetch daily transactions
    fetch("/api/transactions")
      .then((response) => response.json())
      .then((data) => {
        setTransactions(data);
      });
  }, []);

  return (
    <>
      <h1>Transactions</h1>
      <ul>
        {transactions.map((item) => (
          <li key={item.id} onClick={() => setSelectedTransactionId(item.id)}>
            <TransactionSummaryView
              category={item.category}
              amount={item.amount}
              merchant={item.merchant}
              note={item.note}
            />
          </li>
        ))}
      </ul>
      <button onClick={() => setSelectedTransactionId(-1)}>
        Add Transaction
      </button>
    </>
  );
}

export default DailyTransactionListView;
