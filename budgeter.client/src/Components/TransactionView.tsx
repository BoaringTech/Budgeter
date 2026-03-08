import type { Transaction } from "../Interfaces/Transaction";
import DatePicker from "react-datepicker";
import TimePicker from "react-time-picker";
import { useState } from "react";

import "react-time-picker/dist/TimePicker.css";
import "react-clock/dist/Clock.css";

function TransactionView({
  id,
  user,
  dateTime,
  account,
  transactionType,
  category,
  subcategory,
  amount,
  note,
}: Transaction) {
  const [changedUser, setUser] = useState(user);
  const [changedTransactionType, setTransactionType] =
    useState(transactionType);
  const [changedDate, setDate] = useState(dateTime);
  const [changedTime, setTime] = useState(dateTime);
  const [changedAccount, setAccount] = useState(account);
  const [changedCategory, setCategory] = useState(category);
  const [changedSubcategory, setSubcategory] = useState(subcategory);
  const [changedAmount, setAmount] = useState(amount);
  const [changedNote, setNote] = useState(note);

  return (
    <>
      <h1>Transaction</h1>
      <button
        disabled={changedTransactionType === "Income"}
        onClick={() => setTransactionType("Income")}
      >
        Income
      </button>
      <button
        disabled={changedTransactionType === "Expense"}
        onClick={() => setTransactionType("Expense")}
      >
        Expense
      </button>
      <button
        disabled={changedTransactionType === "Transfer"}
        onClick={() => setTransactionType("Transfer")}
      >
        Transfer
      </button>
      <table>
        <tr>
          <td>Date</td>
          <DatePicker
            selected={changedDate}
            onChange={(date: Date | null) => setDate(SetDateAssumeNow(date))}
          />
          <TimePicker
            value={changedTime}
            onChange={(time) => setTime(SetTimeAssumeNow(time))}
          />
        </tr>
        <tr>
          <td>User</td>
          <input
            type="text"
            value={changedUser || ""}
            onChange={(e) => setUser(e.target.value)}
          />
        </tr>
        <tr>
          <td>Account</td>
          <input
            type="text"
            value={changedAccount || ""}
            onChange={(e) => setAccount(e.target.value)}
          />
        </tr>
        <tr>
          <td>Category</td>
          <input
            type="text"
            value={changedCategory || ""}
            onChange={(e) => setCategory(e.target.value)}
          />
        </tr>
        <tr>
          <td>Subcategory</td>
          <input
            type="text"
            value={changedSubcategory || ""}
            onChange={(e) => setSubcategory(e.target.value)}
          />
        </tr>
        <tr>
          <td>Amount</td>
          <input
            type="number"
            value={changedAmount || 0}
            onChange={(e) => setAmount(parseFloat(e.target.value) || 0)}
          />
        </tr>
        <tr>
          <td>Note</td>
          <input
            type="text"
            value={changedNote || ""}
            onChange={(e) => setNote(e.target.value)}
          />
        </tr>
      </table>
    </>
  );
}

function SetDateAssumeNow(date: Date | null): Date {
  if (!date) {
    return new Date(Date.now());
  }

  return date;
}

function SetTimeAssumeNow(time: string | null): Date {
  if (!time) {
    return new Date(Date.now());
  }

  return new Date(time);
}

export default TransactionView;
