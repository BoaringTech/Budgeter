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
  merchant: string | null;
  bookmarked: boolean;
  note: string | null;
}
