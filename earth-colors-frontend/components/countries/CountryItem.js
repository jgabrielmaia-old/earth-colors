import styles from "./CountryItem.module.css";
import { useState } from "react";
import Picker from "./Picker";

export default function CountryItem(props) {
  const [displayPicker, setDisplayPicker] = useState(false);
  const [colorPicked, setColorPicked] = useState(props.color);

  const handleColorPicked = (color, event) => {
    setColorPicked(color.hex)
    console.log("Picked", color.hex)
  }

  return (
    <div>     
      <button
        className={styles.card}
        style={{ backgroundColor: colorPicked }}
        onClick={() => setDisplayPicker(!displayPicker)}
      >
        <h2>{props.name}</h2>
        <p>3244 votes</p>
      </button>
      {displayPicker && <Picker colors={props.colors} color={colorPicked} onChange={handleColorPicked}/>}
    </div>
  );
}
