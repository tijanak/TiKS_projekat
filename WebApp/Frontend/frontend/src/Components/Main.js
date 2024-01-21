import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import { Container } from "@mui/material";
import { useNavigate } from "react-router-dom";
function Main() {
  const [posts, setPosts] = useState(null);
  useEffect(() => {
    fetch(`${BACKEND}Slucaj/Get/All`, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setPosts(data);
      })
      .catch((error) => console.log(error));
  }, []);
  let navigate = useNavigate();
  return (
    <Container>
      <h2>Svi slucajevi({posts && posts.length}):</h2>
      {posts && (
        <>
          {posts.map((p) => (
            <Card key={p.id} sx={{ maxWidth: 345 }}>
              <CardActions>
                <Button size="small">Doniraj</Button>

                <Button size="small">Udomi</Button>
              </CardActions>
              <CardMedia
                sx={{ height: 140 }}
                image="https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.istockphoto.com%2Fphotos%2Fplaceholder-image&psig=AOvVaw3iu14a8dxZufPTObHcDmUR&ust=1705892562250000&source=images&cd=vfe&ved=0CBMQjRxqFwoTCKDv5ZO_7YMDFQAAAAAdAAAAABAE"
                title="slika"
              />
              <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                  {p.naziv}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  {p.opis}
                </Typography>
              </CardContent>
              <CardActions>
                <Button
                  size="small"
                  onClick={() => {
                    navigate("/post", { state: { post: p } });
                  }}
                >
                  Novosti
                </Button>
              </CardActions>
            </Card>
          ))}
        </>
      )}
    </Container>
  );
}
export default Main;
