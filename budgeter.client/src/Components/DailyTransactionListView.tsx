import { useState, useEffect } from "react";
import type { Transaction } from "../Interfaces/Transaction";
import TransactionView from "./TransactionView";

function DailyTransactionListView() {
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
      <ul></ul>
    </>
  );
}

export default DailyTransactionListView;
