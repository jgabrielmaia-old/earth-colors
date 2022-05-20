db.createCollection("votes", { capped: false });
db.votes.createIndexes([
  { _id: 1, countryId: 1 }
], { unique: true });