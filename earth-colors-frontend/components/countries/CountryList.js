import styles from "./CountryList.module.css";
import CountryItem from "./CountryItem";

export default function CountryList(props) {
  return (
    <div className={styles.grid}>
      {props.countries.map(({ id, name, color, colors }) => (
        <CountryItem key={id} id={id} name={name} color={color} colors={colors} />
      ))}
    </div>
  );
}
