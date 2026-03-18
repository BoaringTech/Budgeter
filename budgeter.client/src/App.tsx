import { useState, useEffect } from "react";
import type { Transaction } from "./Interfaces/Transaction";
import TransactionView from "./Components/TransactionView";
import AppState from "./Enums/AppState";
import BookmarksView from "./Components/BookmarksView";
import MainView from "./Components/MainView";

import "./App.css";

function App() {
  const [appState, setAppState] = useState<AppState>(
    AppState.DailyTransactionListView,
  );
  const [viewingBookmarks, setViewingBookmarks] = useState(false);
  const [selectedTransactionId, setSelectedTransactionId] = useState<
    number | null
  >(null);
  const [selectedTransaction, setSelectedTransaction] =
    useState<Transaction | null>(null);

  // Get transaction to display
  useEffect(() => {
    // Skip if selectedTransactionId is null
    if (selectedTransactionId == null) {
      return;
    }

    // Fetch an existing transaction
    if (selectedTransactionId > -1) {
      fetch("/api/transactions/" + selectedTransactionId)
        .then((response) => response.json())
        .then((data) => {
          setSelectedTransaction(data);
          setAppState(AppState.TransactionView);
        });
      return;
    }

    // Create a new transaction
    const newTransaction: Transaction = {
      id: -1,
      userName: null,
      dateTime: new Date(),
      accountName: null,
      transactionType: "Expense",
      categoryName: null,
      subcategoryName: null,
      amount: 0,
      merchant: null,
      bookmarked: false,
      notes: null,
    };
    setSelectedTransaction(newTransaction);
    setAppState(AppState.TransactionView);
  }, [selectedTransactionId]);

  useEffect(() => {}, [appState]);

  return (
    // Display the list of transactions when nothing is selected and loaded
    // or display the transaction details that is selected and loaded
    <>
      {appState !== AppState.TransactionView &&
        appState !== AppState.BookmarksView && (
          <MainView
            appState={appState}
            setSelectedTransactionId={setSelectedTransactionId}
            setAppState={setAppState}
            setViewingBookmarks={setViewingBookmarks}
          />
        )}
      {appState == AppState.TransactionView && selectedTransaction && (
        <TransactionView
          id={selectedTransaction.id}
          user={selectedTransaction.userName}
          dateTime={selectedTransaction.dateTime}
          account={selectedTransaction.accountName}
          transactionType={selectedTransaction.transactionType}
          category={selectedTransaction.categoryName}
          subcategory={selectedTransaction.subcategoryName}
          amount={selectedTransaction.amount}
          merchant={selectedTransaction.merchant}
          bookmarked={selectedTransaction.bookmarked}
          note={selectedTransaction.notes}
          viewingBookmarks={viewingBookmarks}
          setSelectedTransactionId={setSelectedTransactionId}
          setSelectedTransaction={setSelectedTransaction}
          setAppState={setAppState}
        />
      )}
      {appState === AppState.BookmarksView && (
        <BookmarksView
          appState={appState}
          setSelectedTransactionId={setSelectedTransactionId}
          setAppState={setAppState}
          setViewingBookmarks={setViewingBookmarks}
        />
      )}
    </>
  );
}

export default App;
