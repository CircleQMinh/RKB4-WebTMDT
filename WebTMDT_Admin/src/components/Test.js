import { React,useState } from "react";

function Test(props) {
  const name = props.name
  return (
    <div>
      <p>Hello {name}</p>
    </div>
  );
}

export default Test;
