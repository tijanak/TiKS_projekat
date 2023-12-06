import React, { useState, useEffect } from "react";
function Joke() {
  const [joke, setJoke] = useState(null);
  useEffect(() => {
    fetch("http://localhost:5100/Test/Uzmi/1", {
      method: "GET",
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setJoke(data.nebitno);
      })
      .catch((error) => console.log(error));
  }, []);
  return (
    <div>
      <h2>nesto iz baze:</h2>
      {joke && <p>{joke}</p>}
    </div>
  );
}
export default Joke;
