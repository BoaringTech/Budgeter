interface props {
  date: Date;
  setDate: (date: Date) => void;
  showDate: (date: Date) => string;
  increment: (date: Date) => Date;
  decrement: (date: Date) => Date;
}

function DateNavigationButtons({
  date,
  setDate,
  showDate,
  increment,
  decrement,
}: props) {
  return (
    <span>
      <button
        className="back-forward-button"
        onClick={() => {
          setDate(decrement(date));
        }}
      >
        {"<"}
      </button>
      <label>{showDate(date)}</label>
      <button
        className="back-forward-button"
        onClick={() => {
          setDate(increment(date));
        }}
      >
        {">"}
      </button>
    </span>
  );
}

export default DateNavigationButtons;
