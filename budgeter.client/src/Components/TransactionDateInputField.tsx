import { useState } from "react";

interface props {
  label: string;
  property: Date | null;
  setProperty: (p: Date) => void;
}

function TransactionDateInputField({ label, property, setProperty }: props) {
  const getDateString = (date: Date | null): string => {
    if (!date) {
      const now = new Date();
      const year = now.getFullYear();
      const month = String(now.getMonth() + 1).padStart(2, "0");
      const day = String(now.getDate()).padStart(2, "0");
      return `${year}-${month}-${day}`;
    }

    const dateObj = date instanceof Date ? date : new Date(date);
    const year = dateObj.getFullYear();
    const month = String(dateObj.getMonth() + 1).padStart(2, "0");
    const day = String(dateObj.getDate()).padStart(2, "0");
    return `${year}-${month}-${day}`;
  };

  const getTimeString = (date: Date | null): string => {
    if (!date) {
      const now = new Date();
      const hours = String(now.getHours()).padStart(2, "0");
      const minutes = String(now.getMinutes()).padStart(2, "0");
      return `${hours}:${minutes}`;
    }

    const dateObj = date instanceof Date ? date : new Date(date);
    const hours = String(dateObj.getHours()).padStart(2, "0");
    const minutes = String(dateObj.getMinutes()).padStart(2, "0");
    return `${hours}:${minutes}`;
  };

  const [dateValue, setDateValue] = useState<string>(getDateString(property));
  const [timeValue, setTimeValue] = useState<string>(getTimeString(property));

  const createDateFromInputs = (dateStr: string, timeStr: string): Date => {
    if (!dateStr) return new Date();

    // Default time to midnight if not provided
    const time = timeStr || "00:00";
    const [hours, minutes] = time.split(":").map(Number);
    const [year, month, day] = dateStr.split("-").map(Number);

    // Create date object (months are 0-indexed in JavaScript)
    const newDate = new Date(year, month - 1, day, hours, minutes);

    // Validate the date
    if (isNaN(newDate.getTime())) {
      return new Date();
    }

    return newDate;
  };

  const handleDateChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newDateStr = e.target.value;
    setDateValue(newDateStr);

    const newDate = createDateFromInputs(newDateStr, timeValue);
    setProperty(newDate);
  };

  const handleTimeChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newTimeStr = e.target.value;
    setTimeValue(newTimeStr);

    const newDate = createDateFromInputs(dateValue, newTimeStr);
    setProperty(newDate);
  };

  return (
    <div className="transactionInputField">
      <label className="label">{label}</label>
      <input
        name={label}
        type="date"
        value={dateValue}
        onChange={handleDateChange}
      />
      <input
        name={label}
        type="time"
        value={timeValue}
        onChange={handleTimeChange}
      />
    </div>
  );
}

export default TransactionDateInputField;
