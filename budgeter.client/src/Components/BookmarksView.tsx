import { useState, useEffect } from "react";
import type { Transaction } from "../Interfaces/Transaction";
import DatedTransactionSummaryView from "./DatedTransactionSummaryView";
import AppState from "../Enums/AppState";

import "../StyleSheets/TransactionSummary.css";
import "../StyleSheets/DailyTransactionListView.css";

interface props {
  appState: AppState;
  setSelectedTransactionId: (transaction: number | null) => void;
  setAppState: (appState: AppState) => void;
}

function BookmarksView({
  appState,
  setSelectedTransactionId,
  setAppState,
}: props) {
  const [transactions, setTransactions] = useState<Transaction[]>([]);

  useEffect(() => {
    // Fetch daily transactions
    fetch("/api/transactions/bookmarks")
      .then((response) => response.json())
      .then((data) => {
        setTransactions(data);
      });
  }, [appState]);

  const showTransaction = (id: number) => {
    setSelectedTransactionId(id);
    setAppState(AppState.TransactionView);
  };

  return (
    <>
      <header>
        <button onClick={() => setAppState(AppState.DailyTransactionListView)}>
          {"<"}
        </button>
        <label>Bookmarks</label>
      </header>
      <main>
        <div className="transactions-page">
          <div className="transactions-container">
            {transactions.map((item) => (
              <button
                key={item.id}
                onClick={() => showTransaction(item.id)}
                className="transaction-summary"
              >
                <DatedTransactionSummaryView
                  date={item.dateTime}
                  category={item.categoryName}
                  amount={item.amount}
                  merchant={item.merchant}
                  notes={item.notes}
                />
              </button>
            ))}
          </div>
        </div>
      </main>
    </>
  );
}

function showMonthAndYear(date: Date): string {
  return date.toLocaleDateString("en-US", {
    month: "short",
    year: "numeric",
  });
}

export default BookmarksView;
