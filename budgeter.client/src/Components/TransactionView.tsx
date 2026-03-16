import { useState } from "react";
import type { Transaction } from "../Interfaces/Transaction";
import type { TransactionTypes } from "../Enums/TransactionTypes";
import TransactionTypeSelection from "./TransactionTypeSelection";
import TransactionStringInputField from "./TransactionStringInputField";
import TransactionNumberInputField from "./TransactionNumberInputField";

import "react-time-picker/dist/TimePicker.css";
import "react-clock/dist/Clock.css";
import TransactionSaveButtons from "./TransactionSaveButtons";
import TransactionButtonInputField from "./TransactionButtonInputField";
import TransactionDateInputField from "./TransactionDateInputField";
import type { UpdateTransaction } from "../Interfaces/UpdateTransaction";

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
  const updateTransaction = async (newTransaction: UpdateTransaction) => {
    setSaving(true);
    setError(null);
    setSuccess(false);

    try {
      // PUT transaction
      const response = await fetch("/api/transactions/" + id, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
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
    if (saving) return;

    if (id > -1) {
      const updatedTransaction: UpdateTransaction = {
        userName: changedUser,
        dateTime: changedDateTime,
        accountName: changedAccount,
        transactionType: changedTransactionType,
        categoryName: changedCategory,
        subcategoryName: changedSubcategory,
        amount: changedAmount,
        merchant: changedMerchant,
        bookmarked: changedBookmarked,
        notes: changedNote,
      };
      updateTransaction(updatedTransaction);
    } else {
      const newTransaction: Transaction = {
        id: id,
        userName: changedUser,
        dateTime: changedDateTime,
        accountName: changedAccount,
        transactionType: changedTransactionType,
        categoryName: changedCategory,
        subcategoryName: changedSubcategory,
        amount: changedAmount,
        merchant: changedMerchant,
        bookmarked: changedBookmarked,
        notes: changedNote,
      };

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
          <TransactionTypeSelection
            selectedTransactionType={changedTransactionType}
            setTransactionType={setTransactionType}
          />
          <TransactionDateInputField
            label="Date"
            property={changedDateTime}
            setProperty={setDateTime}
          />
          <TransactionStringInputField
            label="User"
            property={changedUser}
            setProperty={setUser}
          />
          <TransactionStringInputField
            label="Account"
            property={changedAccount}
            setProperty={setAccount}
          />
          <TransactionStringInputField
            label="Category"
            property={changedCategory}
            setProperty={setCategory}
          />
          <TransactionStringInputField
            label="Subcategory"
            property={changedSubcategory}
            setProperty={setSubcategory}
          />
          <TransactionNumberInputField
            label="Amount"
            property={changedAmount}
            setProperty={setAmount}
          />
          <TransactionStringInputField
            label="Merchant"
            property={changedMerchant}
            setProperty={setMerchant}
          />
          <TransactionStringInputField
            label="Note"
            property={changedNote}
            setProperty={setNote}
          />
          <TransactionButtonInputField
            label="Bookmark"
            property={changedBookmarked}
            setProperty={setBookmarked}
          />
          <TransactionSaveButtons
            saving={saving}
            onSave={saveTransaction}
            onCancel={traverseBack}
          />
        </form>
      </main>
    </>
  );
}

export default TransactionView;
