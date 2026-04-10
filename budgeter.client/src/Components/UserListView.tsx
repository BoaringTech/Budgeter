import { useState } from "react";
import type { User } from "../Interfaces/User";

import "../StyleSheets/AdjustableList.css";

interface props {
  users: User[];
}

function UserListView({ users: initialUsers }: props) {
  const [users, setUsers] = useState(initialUsers);
  const [editingId, setEditingId] = useState(-1);
  const [editValue, setEditValue] = useState("");

  const startEditing = (user: User) => {
    setEditingId(user.id);
    setEditValue(user.name);
  };

  const saveEdit = () => {
    if (editValue.trim() !== "") {
      setUsers(
        users.map((user) =>
          user.id === editingId ? { ...user, name: editValue } : user,
        ),
      );
    }
    setEditingId(-1);
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === "Enter") {
      saveEdit();
    } else if (e.key === "Escape") {
      setEditingId(-1);
    }
  };

  const handleAdd = (e: React.MouseEvent<HTMLButtonElement>) => {
    const newUser: User = {
      id: Date.now(),
      name: "New User",
      isSystem: false,
      order: users.length,
    };
    setUsers([...users, newUser]);
  };

  const handleDelete = (id: number) => {
    setUsers(users.filter((user) => user.id !== id));
  };

  return (
    <>
      {users
        .filter((user) => !user.isSystem)
        .map((item) => (
          <div key={item.id} className="account-item">
            <span>
              <button>Edit</button>
              <text>{item.name}</text>
            </span>
            <span>
              <button>^</button>
              <button>v</button>
              <button onClick={() => handleDelete(item.id)}>del</button>
            </span>
          </div>
        ))}
      <button onClick={handleAdd}>Add</button>
    </>
  );
}

export default UserListView;
