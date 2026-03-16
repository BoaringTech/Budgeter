import { useState, useEffect } from "react";
import type { Transaction } from "./Interfaces/Transaction";
import DailyTransactionListView from "./Components/DailyTransactionListView";
import TransactionView from "./Components/TransactionView";

import "./App.css";
import "react-datepicker/dist/react-datepicker.css";

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
      fetch("/api/transactions/" + selectedTransactionId)
        .then((response) => response.json())
        .then((data) => {
          setSelectedTransaction(data);
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
  }, [selectedTransactionId]);

  return (
    // Display the list of transactions when nothing is selected and loaded
    // or display the transaction details that is selected and loaded
    <>
      {!selectedTransaction && (
        <DailyTransactionListView
          setSelectedTransactionId={setSelectedTransactionId}
          refreshTrigger={refreshDate}
        />
      )}
      {selectedTransaction && (
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
          setSelectedTransactionId={setSelectedTransactionId}
          setSelectedTransaction={setSelectedTransaction}
          setRefreshDate={setRefreshDate}
        />
      )}
    </>
  );
}

export default App;
