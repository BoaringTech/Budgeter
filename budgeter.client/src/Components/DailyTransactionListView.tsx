import { useState, useEffect } from "react";
import type { Transaction } from "../Interfaces/Transaction";
import TransactionSummaryView from "./TransactionSummaryView";
import AppState from "../Enums/AppState";

import "../StyleSheets/DailyTransactionListView.css";

interface props {
  appState: AppState;
  setSelectedTransactionId: (transaction: number | null) => void;
  setAppState: (appState: AppState) => void;
}

function DailyTransactionListView({
  appState,
  setSelectedTransactionId,
  setAppState,
}: props) {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [month, setMonth] = useState(getMonthAndYear(new Date()));

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
      <header>
        <div>
          <button
            onClick={() => {
              setMonth(getMonthAndYear(decrementMonth(month)));
            }}
          >
            {"<"}
          </button>
          <label>{showMonthAndYear(month)}</label>
          <button
            onClick={() => {
              setMonth(getMonthAndYear(incrementMonth(month)));
            }}
          >
            {">"}
          </button>
          <button>Search</button>
          <button>Settings</button>
        </div>
        <div>
          <button>Daily</button>
          <button>Calendar</button>
          <button>Weekly</button>
          <button onClick={() => setAppState(AppState.BookmarksView)}>
            Bookmarks
          </button>
          <button>Notes</button>
        </div>
      </header>
      <main>
        <div className="transactions-page">
          <div className="transactions-container">
            {transactions.map((item) => (
              <button
                key={item.id}
                onClick={() => setSelectedTransactionId(item.id)}
                className="transaction-summary"
              >
                <TransactionSummaryView
                  category={item.categoryName}
                  amount={item.amount}
                  merchant={item.merchant}
                  notes={item.notes}
                />
              </button>
            ))}
          </div>
          <button onClick={() => setSelectedTransactionId(-1)}>
            Add Transaction
          </button>
        </div>
      </main>
    </>
  );
}

function getMonthAndYear(date: Date): Date {
  let year = date.getFullYear();
  let month = date.getMonth();
  return new Date(year, month);
}

function showMonthAndYear(date: Date): string {
  return date.toLocaleDateString("en-US", {
    month: "short",
    year: "numeric",
  });
}

function decrementMonth(date: Date): Date {
  return new Date(date.setMonth(date.getMonth() - 1));
}

function incrementMonth(date: Date): Date {
  return new Date(date.setMonth(date.getMonth() + 1));
}

export default DailyTransactionListView;
