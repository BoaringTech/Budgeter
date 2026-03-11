import { useState, useEffect } from "react";
import type { Transaction } from "./Interfaces/Transaction";
import DailyTransactionListView from "./Components/DailyTransactionListView";
import TransactionView from "./Components/TransactionView";

import "./App.css";

function App() {
  const [selectedTransactionId, setSelectedTransactionId] = useState<
    number | null
  >(null);
  const [selectedTransaction, setSelectedTransaction] =
    useState<Transaction | null>(null);
  const [refreshDate, setRefreshDate] = useState(new Date());

  // Get transaction to display
  useEffect(() => {
    // Skip if selectedTransactionId is null
    if (selectedTransactionId == null) {
      return;
    }

    // Fetch an existing transaction
    if (selectedTransactionId > -1) {
      fetch("/transactions/" + selectedTransactionId)
        .then((response) => response.json())
        .then((data) => {
          setSelectedTransaction(data);
        });
      return;
    }

    // Create a new transaction
    const newTransaction: Transaction = {
      id: -1,
      user: null,
      dateTime: new Date(),
      account: null,
      transactionType: "Expense",
      category: null,
      subcategory: null,
      amount: 0,
      merchant: null,
      bookmarked: false,
      note: null,
    };
    setSelectedTransaction(newTransaction);
  }, [selectedTransactionId]);

  return (
    // Display the list of transactions when nothing is selected and loaded
    // or display the transaction details that is selected and loaded
    <>
      {(!selectedTransactionId || !selectedTransaction) && (
        <DailyTransactionListView
          setSelectedTransactionId={setSelectedTransactionId}
          refreshTrigger={refreshDate}
        />
      )}
      {selectedTransactionId && selectedTransaction && (
        <TransactionView
          id={selectedTransaction.id}
          user={selectedTransaction.user}
          dateTime={selectedTransaction.dateTime}
          account={selectedTransaction.account}
          transactionType={selectedTransaction.transactionType}
          category={selectedTransaction.category}
          subcategory={selectedTransaction.subcategory}
          amount={selectedTransaction.amount}
          merchant={selectedTransaction.merchant}
          bookmarked={selectedTransaction.bookmarked}
          note={selectedTransaction.note}
          setSelectedTransactionId={setSelectedTransactionId}
          setSelectedTransaction={setSelectedTransaction}
          setRefreshDate={setRefreshDate}
        />
      )}
    </>
  );
}

export default App;
