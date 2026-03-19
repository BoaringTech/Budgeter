const AppState = {
  DailyTransactionListView: 0,
  TransactionView: 1,
  BookmarksView: 2,
  SettingsView: 3,
};

type AppState = (typeof AppState)[keyof typeof AppState];

export default AppState;
