import type { TransactionTypes } from "../Enums/TransactionTypes";

export interface Category {
  name: string;
  order: number;
  transactionType: TransactionTypes;
}
