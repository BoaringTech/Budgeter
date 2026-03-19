import { useState, useEffect } from "react";
import type { Transaction } from "./Interfaces/Transaction";
import type { BooleanSetting } from "./Interfaces/BooleanSetting";
import TransactionView from "./Components/TransactionView";
import AppState from "./Enums/AppState";
import BookmarksView from "./Components/BookmarksView";
import MainView from "./Components/MainView";
import SettingsView from "./Components/SettingsView";

import "./App.css";
import type { User } from "./Interfaces/User";
import type { Account } from "./Interfaces/Account";

function App() {
  // App Viewing States
  const [appState, setAppState] = useState<AppState>(
    AppState.DailyTransactionListView,
  );
  const [viewingBookmarks, setViewingBookmarks] = useState(false);
  const [selectedTransactionId, setSelectedTransactionId] = useState<
    number | null
  >(null);
  const [selectedTransaction, setSelectedTransaction] =
    useState<Transaction | null>(null);

  const [users, setUsers] = useState<User[]>([]);
  const [accounts, setAccounts] = useState<Account[]>([]);

  // Settings
  const [trackUsers, setTrackUsers] = useState(true);
  const [trackAccounts, setTrackAccounts] = useState(true);

  const [, setSettingsLoading] = useState(false);
  const [, setSettingsError] = useState<string | null>(null);
  const [, setUsersLoading] = useState(false);
  const [, setUsersError] = useState<string | null>(null);
  const [, setAccountsLoading] = useState(false);
  const [, setAccountsError] = useState<string | null>(null);

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

  useEffect(() => {
    const getSettings = async (): Promise<void> => {
      setSettingsLoading(true);
      setSettingsError(null);

      try {
        const response = await fetch("/api/settings/");

        if (!response.ok) {
          throw new Error(
            "Could not retrieve settings! status: ${response.status}",
          );
        }

        const settings: BooleanSetting[] = await response.json();

        settings.forEach((setting) => {
          if (setting.name === "TrackUsers") {
            setTrackUsers(setting.enabled);
          } else if (setting.name === "TrackAccounts") {
            setTrackAccounts(setting.enabled);
          }
        });
      } catch (error) {
        const errorMessage =
          error instanceof Error ? error.message : "Failed to load settings";
        setSettingsError(errorMessage);
      } finally {
        setSettingsLoading(false);
      }
    };

    const getUsers = async (): Promise<void> => {
      setUsersError(null);

      try {
        const response = await fetch("/api/users/");

        if (!response.ok) {
          throw new Error(
            "Could not retreive users! status: ${response.status}",
          );
        }

        const users: User[] = await response.json();
        setUsers(users);
      } catch (error) {
        const errorMessage =
          error instanceof Error ? error.message : "Failed to load users";
        setUsersError(errorMessage);
      } finally {
        setUsersLoading(false);
      }
    };

    const getAccounts = async (): Promise<void> => {
      setAccountsError(null);

      try {
        const response = await fetch("/api/accounts/");

        if (!response.ok) {
          throw new Error(
            "Could not retreive accounts! status: ${response.status}",
          );
        }

        const accounts: Account[] = await response.json();
        setAccounts(accounts);
      } catch (error) {
        const errorMessage =
          error instanceof Error ? error.message : "Failed to load accounts";
        setAccountsError(errorMessage);
      } finally {
        setAccountsLoading(false);
      }
    };

    getSettings();
    getUsers();
    getAccounts();
  }, []);

  return (
    // Display the list of transactions when nothing is selected and loaded
    // or display the transaction details that is selected and loaded
    <>
      {appState === AppState.DailyTransactionListView && (
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
          trackAccounts={trackAccounts}
          trackUsers={trackUsers}
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
      {appState === AppState.SettingsView && (
        <SettingsView
          setAppState={setAppState}
          trackUsers={trackUsers}
          trackAccounts={trackAccounts}
          users={users}
          accounts={accounts}
          setTrackUsers={setTrackUsers}
          setTrackAccounts={setTrackAccounts}
        />
      )}
    </>
  );
}

export default App;
