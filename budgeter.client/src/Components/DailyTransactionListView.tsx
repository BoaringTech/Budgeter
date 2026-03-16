import { useState, useEffect } from "react";
import type { Transaction } from "../Interfaces/Transaction";
import TransactionSummaryView from "./TransactionSummaryView";

import "../StyleSheets/TransactionSummary.css";
import "../StyleSheets/DailyTransactionListView.css";

interface props {
  setSelectedTransactionId: (transaction: number | null) => void;
  refreshTrigger: Date;
}

function DailyTransactionListView({
  setSelectedTransactionId,
  refreshTrigger,
}: props) {
  const [transactions, setTransactions] = useState<Transaction[]>([]);

  useEffect(() => {
    // Fetch daily transactions
    fetch("/api/transactions")
      .then((response) => response.json())
      .then((data) => {
        setTransactions(data);
      });
  }, [refreshTrigger]);

  return (
    <>
      <div className="transactions-page">
        <h1>Transactions</h1>
        <div className="transactions-container">
          {transactions.map((item) => (
            <div
              key={item.id}
              onClick={() => setSelectedTransactionId(item.id)}
              className="transactionSummary"
            >
              <TransactionSummaryView
                category={item.category}
                amount={item.amount}
                merchant={item.merchant}
                notes={item.notes}
              />
            </div>
          ))}
        </div>
        <button onClick={() => setSelectedTransactionId(-1)}>
          Add Transaction
        </button>
      </div>
    </>
  );
}

export default DailyTransactionListView;
