import React, { useState, useEffect } from "react";

export function Novost(id_novosti) {
  const [novost, setNovost] = useState(null);
  useEffect(() => {
    fetch(`http://localhost:${window.location.port}/Novost/preuzminovost/${id_novosti}`, {
      method: "GET",
    })
      .then(response => response.json())
      .then(data => {
        console.log(data);
        setPost(data);
      })
      .catch((error) => console.log(error));
  }, []);
  return (
    <>{
        (novost&&
        <div>
            <h2>{novost.Datum}</h2>
            <p>{novost.Tekst}</p>
            {Slika&&<img src={novost.Slika} alt="slika"></img>}
        </div>)
        ||<>ne ucitava novost</>
    }</>
  );
}