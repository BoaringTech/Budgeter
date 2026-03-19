import type { Account } from "../Interfaces/Account";

interface props {
  accounts: Account[];
}

function AccountListView({ accounts }: props) {
  return (
    <ul>
      {accounts.map((item) => (
        <li key={item.id}>{item.name}</li>
      ))}
    </ul>
  );
}

export default AccountListView;
