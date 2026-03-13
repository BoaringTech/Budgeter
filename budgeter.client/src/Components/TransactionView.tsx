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
  const [saving, setSaving] = useState(false);
  const [, setError] = useState(null);
  const [, setSuccess] = useState(false);

  // Create Transaction Function
  const createTransaction = async (newTransaction: Transaction) => {
    setSaving(true);
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
      setSaving(false);
      traverseBack();
    }
  };

  // Update Transaction Function
  const updateTransaction = async (newTransaction: Transaction) => {
    setSaving(true);
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
      setSaving(false);
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

    if (saving) return;

    if (id > -1) {
      updateTransaction(newTransaction);
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
      <main>
        <form>
          <tr className="transactionFormButtons">
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
          </tr>
          <tr>
            <th className="label">Date</th>
            <td>
              <DatePicker
                selected={changedDateTime}
                showTimeSelect
                dropdownMode="select"
                onChange={(date: Date | null) =>
                  setDateTime(SetDateAssumeNow(date))
                }
              />
            </td>
          </tr>
          <tr>
            <th className="label">User</th>
            <td>
              <input
                name="User"
                type="text"
                value={changedUser || ""}
                onChange={(e) => setUser(e.target.value)}
              />
            </td>
          </tr>
          <tr>
            <th className="label">Account</th>
            <td className="input">
              <input
                name="Account"
                type="text"
                value={changedAccount || ""}
                onChange={(e) => setAccount(e.target.value)}
              />
            </td>
          </tr>
          <tr>
            <th className="label">Category</th>
            <td>
              <input
                name="Category"
                type="text"
                value={changedCategory || ""}
                onChange={(e) => setCategory(e.target.value)}
              />
            </td>
          </tr>
          <tr>
            <th className="label">Subcategory</th>
            <td>
              <input
                name="Subcategory"
                type="text"
                value={changedSubcategory || ""}
                onChange={(e) => setSubcategory(e.target.value)}
              />
            </td>
          </tr>
          <tr>
            <th className="label">Amount</th>
            <td>
              <input
                name="Amount"
                type="number"
                value={changedAmount || 0}
                onChange={(e) => setAmount(parseFloat(e.target.value) || 0)}
              />
            </td>
          </tr>
          <tr>
            <th className="label">Merchant</th>
            <td>
              <input
                name="Merchant"
                type="text"
                value={changedMerchant || ""}
                onChange={(e) => setMerchant(e.target.value)}
              />
            </td>
          </tr>
          <tr>
            <th className="label">Note</th>
            <td>
              <input
                name="Note"
                type="text"
                value={changedNote || ""}
                onChange={(e) => setNote(e.target.value)}
              />
            </td>
          </tr>
          <tr>
            <th className="label">Bookmark</th>
            <td>
              <input
                name="Bookmark"
                type="checkbox"
                checked={changedBookmarked}
                onChange={(e) => setBookmarked(OnCheckboxChanged(e))}
              />
            </td>
          </tr>
          <tr className="transactionFormButtons">
            <button onClick={saveTransaction}>
              {!saving ? "Save" : "Saving..."}
            </button>
            <button onClick={traverseBack}>Cancel</button>
          </tr>
        </form>
      </main>
    </>
  );
}

function SetDateAssumeNow(date: Date | null): Date {
  if (!date) {
    return new Date(Date.now());
  }

  return date;
}

function OnCheckboxChanged(e: ChangeEvent<HTMLInputElement>): boolean {
  return e.target.checked;
}

export default TransactionView;
