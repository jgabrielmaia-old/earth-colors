import { BlockPicker } from "react-color";

export default function Picker(props) {
    return (
        <BlockPicker colors={props.colors} color={props.color} onChangeComplete={props.onChange} />
    );
  }