import { useState } from "react";
import AppState from "../Enums/AppState";
import DailyTransactionListView from "./DailyTransactionListView";
import type { Transaction } from "../Interfaces/Transaction";
import DateNavigationButtons from "./DateNavigationButtons";

interface props {
  appState: AppState;
  setSelectedTransactionId: (transaction: number | null) => void;
  setAppState: (appState: AppState) => void;
  setViewingBookmarks: (viewingBookmarks: boolean) => void;
}

function MainView({
  appState,
  setSelectedTransactionId,
  setAppState,
  setViewingBookmarks,
}: props) {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [month, setMonth] = useState(getMonthAndYear(new Date()));

  const viewBookmarks = () => {
    setAppState(AppState.BookmarksView);
    setViewingBookmarks(true);
  };

  return (
    <>
      <header>
        <div className="search-nav-buttons">
          {appState === AppState.DailyTransactionListView && (
            <DateNavigationButtons
              date={month}
              setDate={() => setMonth(getMonthAndYear(month))}
              showDate={showMonthAndYear}
              increment={incrementMonth}
              decrement={decrementMonth}
            />
          )}
          <span>
            <button>Settings</button>
            <button>Search</button>
          </span>
        </div>
        <div className="main-nav-buttons">
          <button
            className={
              appState === AppState.DailyTransactionListView
                ? "selected-button"
                : ""
            }
          >
            Daily
          </button>
          <button>Weekly</button>
          <button>Calendar</button>
          <button>Budget</button>
          <button onClick={viewBookmarks}>Bookmarks</button>
        </div>
      </header>

      <main>
        {appState === AppState.DailyTransactionListView && (
          <DailyTransactionListView
            appState={appState}
            transactions={transactions}
            month={month}
            setTransactions={setTransactions}
            setSelectedTransactionId={setSelectedTransactionId}
          />
        )}
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

export default MainView;
