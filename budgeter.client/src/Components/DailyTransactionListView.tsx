import { useState, useEffect } from "react";
import type { Transaction } from "../Interfaces/Transaction";
import AppState from "../Enums/AppState";
import DatedTransactionSummaryView from "./DatedTransactionSummaryView";

import "../StyleSheets/DailyTransactionListView.css";

interface props {
  appState: AppState;
  month: Date;
  setSelectedTransactionId: (transaction: number | null) => void;
}

function DailyTransactionListView({
  appState,
  month,
  setSelectedTransactionId,
}: props) {
  const [transactions, setTransactions] = useState<Transaction[]>([]);

  useEffect(() => {
    // Fetch daily transactions
    fetch(
      "/api/transactions/time?year=" +
        month.getFullYear() +
        "&month=" +
        (month.getMonth() + 1),
    )
      .then((response) => response.json())
      .then((data) => {
        setTransactions(data);
      });
  }, [appState, month]);

  return (
    <>
      <div className="transactions-page">
        <div className="transactions-container">
          {transactions.map((item) => (
            <button
              key={item.id}
              onClick={() => setSelectedTransactionId(item.id)}
              className="transaction-summary"
            >
              <DatedTransactionSummaryView
                date={item.dateTime}
                type={item.transactionType}
                category={item.categoryName}
                amount={item.amount}
                merchant={item.merchant}
                notes={item.notes}
              />
            </button>
          ))}
        </div>
        <button
          className="transactions-add-transaction-button"
          onClick={() => setSelectedTransactionId(-1)}
        >
          Add Transaction
        </button>
      </div>
    </>
  );
}

export default DailyTransactionListView;
