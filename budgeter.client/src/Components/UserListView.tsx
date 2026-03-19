import type { User } from "../Interfaces/User";

interface props {
  users: User[];
}

function UserListView({ users }: props) {
  return (
    <ul>
      {users.map((item) => (
        <li key={item.id}>{item.name}</li>
      ))}
    </ul>
  );
}

export default UserListView;
