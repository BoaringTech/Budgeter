import DatePicker from "react-datepicker";
import { useState, type ChangeEvent } from "react";
import type { Transaction } from "../Interfaces/Transaction";
import type { TransactionTypes } from "../Enums/TransactionTypes";

import "react-time-picker/dist/TimePicker.css";
import "react-clock/dist/Clock.css";

interface props {
  id: number;
  user: string | null;
  dateTime: Date;
  account: string | null;
  transactionType: TransactionTypes;
  category: string | null;
  subcategory: string | null;
  amount: number;
  merchant: string | null;
  bookmarked: boolean;
  note: string | null;
  setSelectedTransactionId: (id: number | null) => void;
  setSelectedTransaction: (transaction: Transaction | null) => void;
  setRefreshDate: (date: Date) => void;
}

function TransactionView({
  id,
  user,
  dateTime,
  account,
  transactionType,
  category,
  subcategory,
  amount,
  merchant,
  bookmarked,
  note,
  setSelectedTransactionId,
  setSelectedTransaction,
  setRefreshDate,
}: props) {
  // Transaction States
  const [changedUser, setUser] = useState(user);
  const [changedTransactionType, setTransactionType] =
    useState(transactionType);
  const [changedDateTime, setDateTime] = useState(dateTime);
  const [changedAccount, setAccount] = useState(account);
  const [changedCategory, setCategory] = useState(category);
  const [changedSubcategory, setSubcategory] = useState(subcategory);
  const [changedAmount, setAmount] = useState(amount);
  const [changedMerchant, setMerchant] = useState(merchant);
  const [changedBookmarked, setBookmarked] = useState(bookmarked);
  const [changedNote, setNote] = useState(note);

  // Create/Update States
  const [, setLoading] = useState(false);
  const [, setError] = useState(null);
  const [, setSuccess] = useState(false);

  // Create Transaction Function
  const createTransaction = async (newTransaction: Transaction) => {
    setLoading(true);
    setError(null);
    setSuccess(false);

    try {
      // POST transaction
      const response = await fetch("/api/transactions", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newTransaction),
      });

      if (!response.ok) {
        throw new Error(
          "Unable to create transaction! status ${response.status}",
        );
      }

      const data = await response.json();
      setSuccess(true);
      return data;
    } catch (err: any) {
      setError(err.message);
    } finally {
      setLoading(false);
      traverseBack();
    }
  };

  // Update Transaction Function
  const updateTransction = async (newTransaction: Transaction) => {
    setLoading(true);
    setError(null);
    setSuccess(false);

    try {
      // PUT transaction
      const response = await fetch("/transactions/" + newTransaction.id, {
        method: "PUT",
        headers: {
          "Content-Type": "applicaiton/json",
        },
        body: JSON.stringify(newTransaction),
      });

      if (!response.ok) {
        throw new Error(
          "Unable to update transaction! status ${response.status}",
        );
      }

      const data = await response.json();
      setSuccess(true);
      return data;
    } catch (err: any) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  // Saves Transaction Function
  const saveTransaction = () => {
    const newTransaction: Transaction = {
      id: id,
      user: changedUser,
      dateTime: changedDateTime,
      account: changedAccount,
      transactionType: changedTransactionType,
      category: changedCategory,
      subcategory: changedSubcategory,
      amount: changedAmount,
      merchant: changedMerchant,
      bookmarked: changedBookmarked,
      note: changedNote,
    };

    if (id > -1) {
      updateTransction(newTransaction);
    } else {
      createTransaction(newTransaction);
    }
  };

  const traverseBack = () => {
    setSelectedTransactionId(-1);
    setSelectedTransaction(null);
    setRefreshDate(new Date());
  };

  return (
    <>
      <h1>Transaction</h1>
      <span>
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
      </span>
      <span>
        <label>Date</label>
        <DatePicker
          selected={changedDateTime}
          showTimeSelect
          onChange={(date: Date | null) => setDateTime(SetDateAssumeNow(date))}
        />
      </span>
      <span>
        <label>User</label>
        <input
          name="User"
          type="text"
          value={changedUser || ""}
          onChange={(e) => setUser(e.target.value)}
        />
      </span>
      <span>
        <label>Account</label>
        <input
          name="Account"
          type="text"
          value={changedAccount || ""}
          onChange={(e) => setAccount(e.target.value)}
        />
      </span>
      <span>
        <label>Category</label>
        <input
          name="Category"
          type="text"
          value={changedCategory || ""}
          onChange={(e) => setCategory(e.target.value)}
        />
      </span>
      <span>
        <label>Subcategory</label>
        <input
          name="Subcategory"
          type="text"
          value={changedSubcategory || ""}
          onChange={(e) => setSubcategory(e.target.value)}
        />
      </span>
      <span>
        <label>Amount</label>
        <input
          name="Amount"
          type="number"
          value={changedAmount || 0}
          onChange={(e) => setAmount(parseFloat(e.target.value) || 0)}
        />
      </span>
      <span>
        <label>Merchant</label>
        <input
          name="Merchant"
          type="text"
          value={changedMerchant || ""}
          onChange={(e) => setMerchant(e.target.value)}
        />
      </span>
      <span>
        <label>Note</label>
        <input
          name="Note"
          type="text"
          value={changedNote || ""}
          onChange={(e) => setNote(e.target.value)}
        />
      </span>
      <span>
        <label>Bookmark</label>
        <input
          name="Bookmark"
          type="checkbox"
          checked={changedBookmarked}
          onChange={(e) => setBookmarked(OnCheckboxChanged(e))}
        />
      </span>
      <button onClick={saveTransaction}>Save</button>
      <button onClick={traverseBack}>Cancel</button>
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

function OnCheckboxChanged(e: ChangeEvent<HTMLInputElement>): boolean {
  return e.target.checked;
}

function CombineDateAndTime(date: Date, time: Date) {
  return new Date(
    Date.UTC(
      date.getUTCFullYear(),
      date.getUTCMonth(),
      date.getUTCDate(),
      time.getUTCHours(),
      time.getUTCMinutes(),
      time.getUTCSeconds(),
    ),
  );
}

export default TransactionView;
