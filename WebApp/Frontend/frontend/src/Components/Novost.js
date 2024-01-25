import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import { useLocation } from "react-router-dom";

export function Novost() {
  const [novost, setNovost] = useState(null);

  const location = useLocation();
  useEffect(() => {
    setNovost(location.state.novost);
  }, []);
  // const [novost, setNovost] = useState(null);
  // useEffect(() => {
  //   fetch(`${BACKEND}Novost/preuzminovost/${id_novosti}`, {
  //     method: "GET",
  //   })
  //     .then((response) => {
  //       if (response.ok) {
  //         data = response.json();
  //         console.log(data);
  //         setNovost(data);
  //       }
  //     })
  //     .catch((error) => console.log(error));
  // }, []);
  return (
    <>
      {(novost && (
        <div>
          <h2>{novost.Datum}</h2>
          <p>{novost.Tekst}</p>
          {novost.Slika && <img src={novost.Slika} alt="slika"></img>}
        </div>
      )) || <>ne ucitava novost</>}
    </>
  );
}
