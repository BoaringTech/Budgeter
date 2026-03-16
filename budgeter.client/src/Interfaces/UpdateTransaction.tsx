import type { TransactionTypes } from "../Enums/TransactionTypes";

export interface UpdateTransaction {
  userName: string | null;
  dateTime: Date;
  accountName: string | null;
  transactionType: TransactionTypes;
  categoryName: string | null;
  subcategoryName: string | null;
  amount: number;
  merchant: string | null;
  bookmarked: boolean;
  notes: string | null;
}
