import { useRef } from "react";
import AppState from "../Enums/AppState";

import "../StyleSheets/Settings.css";

interface props {
  trackUsers: boolean;
  trackAccounts: boolean;
  setAppState: (appState: AppState) => void;
  setTrackUsers: (trackUsers: boolean) => void;
  setTrackAccounts: (trackAccounts: boolean) => void;
}

function SettingsView({
  trackUsers,
  trackAccounts,
  setAppState,
  setTrackUsers,
  setTrackAccounts,
}: props) {
  const initialTrackAccounts = useRef(trackAccounts);
  const initialTrackUsers = useRef(trackUsers);

  const exit = () => {
    if (
      initialTrackAccounts.current !== trackAccounts ||
      initialTrackUsers.current !== trackUsers
    ) {
      postSettings();
    }

    setAppState(AppState.DailyTransactionListView);
  };

  const setTrackUsersOff = () => {
    setTrackUsers(false);
  };

  const setTrackUsersOn = () => {
    setTrackUsers(true);
  };

  const setTrackAccountsOff = () => {
    setTrackAccounts(false);
  };

  const setTrackAccountsOn = () => {
    setTrackAccounts(true);
  };

  const postSettings = async () => {
    if (initialTrackAccounts.current != trackAccounts) {
      fetch("/api/settings/1", {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ enabled: trackAccounts }),
      });
    }

    if (initialTrackUsers.current != trackUsers) {
      fetch("/api/settings/2", {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ enabled: trackUsers }),
      });
    }
  };

  return (
    <>
      <div className="settings-header">
        <button className="back-forward-button" onClick={exit}>
          {"<"}
        </button>
        <h3>Settings</h3>
      </div>
      <div className="settings">
        <h4>Users</h4>
        <p>Do you want to track spending for multiple people?</p>
        <span className="settings-choices">
          <button
            className={trackUsers ? "active" : ""}
            onClick={setTrackUsersOn}
          >
            Yes
          </button>
          <button
            className={!trackUsers ? "active" : ""}
            onClick={setTrackUsersOff}
          >
            No
          </button>
        </span>
      </div>
      <div className="settings">
        <h4>Accounts</h4>
        <p>Do you want to track spending by accounts?</p>
        <span className="settings-choices">
          <button
            className={trackAccounts ? "active" : ""}
            onClick={setTrackAccountsOn}
          >
            Yes
          </button>
          <button
            className={!trackAccounts ? "active" : ""}
            onClick={setTrackAccountsOff}
          >
            No
          </button>
        </span>
      </div>
      <h4 className="settings">Categories</h4>
    </>
  );
}

export default SettingsView;
