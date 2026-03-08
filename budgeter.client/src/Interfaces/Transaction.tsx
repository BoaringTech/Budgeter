import type { TransactionTypes } from "../Enums/TransactionTypes";

export interface Transaction {
  id: number;
  user: string | null;
  dateTime: Date;
  account: string | null;
  transactionType: TransactionTypes;
  category: string | null;
  subcategory: string | null;
  amount: number;
  note: string | null;
}
