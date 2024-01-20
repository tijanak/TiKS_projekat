import React, { useState, useEffect } from "react";
import { Post } from "./Post";

export function Profil(id_korisnika) {
  const [korisnik, setKorisnik] = useState(null);
  useEffect(() => {
    fetch(`http://localhost:${window.location.port}/Korisnik/preuzmikorisnika/${id_korisnika}`, {
      method: "GET",
    })
      .then(response => response.json())
      .then(data => {
        console.log(data);
        setKorisnik(data);
      })
      .catch((error) => console.log(error));
  }, []);
  return (
    <>{
        korisnik&&
        <div>
            <h1>MOJ PROFIL</h1>
            <h2>{korisnik.Username}</h2>
            
            <h2>Moji slucajevi</h2>
            {korisnik.Slucajevi.forEach(element => {
                <Post id_posta={element.ID}></Post>
            })};

        </div>
        ||<>ne ucitava korisnika</>
    }</>
  );
}