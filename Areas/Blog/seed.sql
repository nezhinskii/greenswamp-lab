-- Очистка таблиц и сброс последовательностей
TRUNCATE TABLE interactions, events, post_tags, posts, tags, users, auth RESTART IDENTITY CASCADE;

-- AUTH
INSERT INTO auth (user_id, username, "NormalizedUserName", "Email", "NormalizedEmail", password_hash, "LastLogin", "ResetToken", "TokenExpiry", "EmailConfirmed", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount", "SecurityStamp", "ConcurrencyStamp") VALUES
(1, 'frogger_vig', 'FROGGER_VIG', 'frogger_vig@example.com', 'FROGGER_VIG@EXAMPLE.COM', '$2b$12$ExampleHash1', '2025-04-10 10:00:00', NULL, NULL, FALSE, NULL, FALSE, FALSE, TRUE, 0, 'security_stamp_1', 'concurrency_stamp_1'),
(2, 'dorm23_frogs', 'DORM23_FROGS', 'dorm23_frogs@example.com', 'DORM23_FROGS@EXAMPLE.COM', '$2b$12$ExampleHash2', '2025-04-10 10:00:00', NULL, NULL, FALSE, NULL, FALSE, FALSE, TRUE, 0, 'security_stamp_2', 'concurrency_stamp_2'),
(3, 'ptst_toad', 'PTST_TOAD', 'ptst_toad@example.com', 'PTST_TOAD@EXAMPLE.COM', '$2b$12$ExampleHash3', '2025-04-10 10:00:00', NULL, NULL, FALSE, NULL, FALSE, FALSE, TRUE, 0, 'security_stamp_3', 'concurrency_stamp_3'),
(4, 'lostnfound', 'LOSTNFOUND', 'lostnfound@example.com', 'LOSTNFOUND@EXAMPLE.COM', '$2b$12$ExampleHash4', '2025-04-10 10:00:00', NULL, NULL, FALSE, NULL, FALSE, FALSE, TRUE, 0, 'security_stamp_4', 'concurrency_stamp_4'),
(5, 'mycelium_maddy', 'MYCELIUM_MADDY', 'mycelium_maddy@example.com', 'MYCELIUM_MADDY@EXAMPLE.COM', '$2b$12$ExampleHash5', '2025-04-10 10:00:00', NULL, NULL, FALSE, NULL, FALSE, FALSE, TRUE, 0, 'security_stamp_5', 'concurrency_stamp_5'),
(6, 'actual_swamp_monster', 'ACTUAL_SWAMP_MONSTER', 'actual_swamp_monster@example.com', 'ACTUAL_SWAMP_MONSTER@EXAMPLE.COM', '$2b$12$ExampleHash6', '2025-04-10 10:00:00', NULL, NULL, FALSE, NULL, FALSE, FALSE, TRUE, 0, 'security_stamp_6', 'concurrency_stamp_6'),
(7, 'user1', 'USER1', 'user1@example.com', 'USER1@EXAMPLE.COM', '$2b$12$ExampleHash7', '2025-04-10 10:00:00', NULL, NULL, FALSE, NULL, FALSE, FALSE, TRUE, 0, 'security_stamp_7', 'concurrency_stamp_7'),
(8, 'user2', 'USER2', 'user2@example.com', 'USER2@EXAMPLE.COM', '$2b$12$ExampleHash8', '2025-04-10 10:00:00', NULL, NULL, FALSE, NULL, FALSE, FALSE, TRUE, 0, 'security_stamp_8', 'concurrency_stamp_8'),
(9, 'user3', 'USER3', 'user3@example.com', 'USER3@EXAMPLE.COM', '$2b$12$ExampleHash9', '2025-04-10 10:00:00', NULL, NULL, FALSE, NULL, FALSE, FALSE, TRUE, 0, 'security_stamp_9', 'concurrency_stamp_9'),
(10, 'user4', 'USER4', 'user4@example.com', 'USER4@EXAMPLE.COM', '$2b$12$ExampleHash10', '2025-04-10 10:00:00', NULL, NULL, FALSE, NULL, FALSE, FALSE, TRUE, 0, 'security_stamp_10', 'concurrency_stamp_10'),
(11, 'user5', 'USER5', 'user5@example.com', 'USER5@EXAMPLE.COM', '$2b$12$ExampleHash11', '2025-04-10 10:00:00', NULL, NULL, FALSE, NULL, FALSE, FALSE, TRUE, 0, 'security_stamp_11', 'concurrency_stamp_11');

-- USERS
INSERT INTO users (user_id, username, display_name, avatar_url, bio, created_at, is_active) VALUES
(1, 'frogger_vig', 'Viggo The Frogger', 'https://i.pravatar.cc/200?u=frogger_vig@pravatar.com', 'Digital artist exploring tech and art.', '2025-03-01 10:00:00', TRUE),
(2, 'dorm23_frogs', 'FroggyFriends', 'https://i.pravatar.cc/100?u=dorm23_frogs@pravatar.com', NULL, '2025-03-01 10:00:00', TRUE),
(3, 'ptst_toad', 'Princess Toadstool', 'https://i.pravatar.cc/100?u=ptst_toad@pravatar.com', NULL, '2025-03-01 10:00:00', TRUE),
(4, 'lostnfound', 'SwampFinder', 'https://i.pravatar.cc/100?u=swamp_finder@pravatar.com', NULL, '2025-03-01 10:00:00', TRUE),
(5, 'mycelium_maddy', 'FungiFanatic', 'https://i.pravatar.cc/100?u=shroom_lady@pravatar.com', NULL, '2025-03-01 10:00:00', TRUE),
(6, 'actual_swamp_monster', 'MossBeard99', 'https://i.pravatar.cc/100?u=swamp_cryptid@pravatar.com', NULL, '2025-03-01 10:00:00', TRUE),
(7, 'user1', 'User One', NULL, NULL, '2025-03-01 10:00:00', TRUE),
(8, 'user2', 'User Two', NULL, NULL, '2025-03-01 10:00:00', TRUE),
(9, 'user3', 'User Three', NULL, NULL, '2025-03-01 10:00:00', TRUE),
(10, 'user4', 'User Four', NULL, NULL, '2025-03-01 10:00:00', TRUE),
(11, 'user5', 'User Five', NULL, NULL, '2025-03-01 10:00:00', TRUE);

-- TAGS
INSERT INTO tags (tag_name, created_at, usage_count) VALUES
('TechMagic', '2025-03-01 10:00:00', 1),
('PixelFrog', '2025-03-01 10:00:00', 1),
('ArtProcess', '2025-03-01 10:00:00', 1),
('GlitchAesthetic', '2025-03-01 10:00:00', 1),
('CreativeCoding', '2025-03-01 10:00:00', 1),
('AIMusic', '2025-03-01 10:00:00', 1),
('StudySwamp', '2025-03-01 10:00:00', 1),
('SwampLife', '2025-03-01 10:00:00', 1),
('SwampLostAndFound', '2025-03-01 10:00:00', 1),
('ShroomBuddies', '2025-03-01 10:00:00', 1),
('SwampMysteries', '2025-03-01 10:00:00', 1);

-- POSTS
INSERT INTO posts (user_id, content, post_type, media_url, media_type, alt_text, thumbnail_url, created_at, parent_post_id) VALUES
(1, 'When the code compiles on the first try', 'video', '/videos/Standard_Mode_a_looed_gif_of_a_pixel_frog_flyi.mp4', 'video', 'Digital painting of a neon frog piloting a cyberpunk airship', 'https://via.placeholder.com/800x450', '2025-04-14 14:00:00', NULL),
(1, 'Debugging my sleep schedule. Send caffeine.', 'image', '/images/4x3_Photo_of_a_messy_desk_with_table.png', 'image', 'Photo of a messy desk with sticky note: "BRAIN.EXE CRASHED"', NULL, '2025-04-14 08:00:00', NULL),
(1, 'Spent 6 hours animating one leaf', 'text', NULL, NULL, NULL, NULL, '2025-04-09 16:00:00', NULL),
(1, 'ADHD + art software = infinite rabbit holes.', 'text', NULL, NULL, NULL, NULL, '2025-04-08 16:00:00', NULL),
(1, 'Teaching AI to generate frog-themed synthwave. 30% bops, 70% cursed.', 'text', NULL, NULL, NULL, NULL, '2025-04-07 16:00:00', NULL),
(1, 'Cyborg future. Present: free motherboard keychain.', 'text', NULL, NULL, NULL, NULL, '2025-04-05 16:00:00', NULL),
(1, 'New mascot for coding tutorials? Yes.', 'text', NULL, NULL, NULL, NULL, '2025-04-03 16:00:00', NULL),
(1, 'Singularity = forgetting how to use a toaster?', 'text', NULL, NULL, NULL, NULL, '2025-04-03 16:00:00', NULL),
(1, 'Brainstorming vs. 3AM delirium. The line blurs.', 'text', NULL, NULL, NULL, NULL, '2025-03-27 16:00:00', NULL),
(1, 'Art and tech DO collide – cha cha slide activated.', 'text', NULL, NULL, NULL, NULL, '2025-03-20 16:00:00', NULL),
(2, 'Study group for bio midterm? 3PM @ lilypad cafe.', 'text', NULL, NULL, NULL, NULL, '2025-04-10 15:31:00', NULL),
(3, 'What’s hopping friends?', 'text', NULL, NULL, NULL, NULL, '2025-04-10 05:00:00', NULL),
(4, 'LOST: Silver bottle near Turtle Pond. Reward: bug cookies!', 'text', NULL, NULL, NULL, NULL, '2025-04-10 15:15:00', NULL),
(5, 'Found glowing mushroom in North Bog. Do NOT lick it.', 'text', NULL, NULL, NULL, NULL, '2025-04-10 15:43:00', NULL),
(6, 'Found weird footprints. New cryptid? Or Dave again?', 'text', NULL, NULL, NULL, NULL, '2025-04-10 15:00:00', NULL),
(1, 'Campus Bonfire Night - cozy evening! ', 'text', NULL, NULL, NULL, NULL, '2025-04-10 10:00:00', NULL),
(1, 'Swamp Chess Tournament - Test your skills!', 'text', NULL, NULL, NULL, NULL, '2025-03-15 10:00:00', NULL);

-- POST TAGS
INSERT INTO post_tags (post_id, tag_id) VALUES
(1, 1), (1, 2),
(3, 3), (3, 4),
(5, 5), (5, 6),
(11, 7),
(12, 8),
(13, 9),
(14, 10),
(15, 11);

-- INTERACTIONS
INSERT INTO interactions (user_id, post_id, interaction_type, created_at, content) VALUES
(2, 1, 'comment', '2025-04-10 14:10:00', 'Nice one!'),
(3, 1, 'reribb', '2025-04-10 14:15:00', NULL),
(2, 2, 'comment', '2025-04-10 08:10:00', 'I feel you!'),
(3, 2, 'comment', '2025-04-10 08:15:00', 'Coffee incoming!'),
(4, 2, 'reribb', '2025-04-10 08:20:00', NULL),
(5, 2, 'reribb', '2025-04-10 08:25:00', NULL),
(2, 8, 'comment', '2025-04-03 16:10:00', 'But can it render toast in 4K?'),
(3, 8, 'comment', '2025-04-03 16:15:00', 'Good point!'),
(4, 8, 'comment', '2025-04-03 16:20:00', 'I disagree.'),
(1, 11, 'comment', '2025-04-10 15:35:00', 'I’m in!'),
(3, 11, 'reribb', '2025-04-10 15:40:00', NULL),
(1, 12, 'comment', '2025-04-10 05:10:00', 'Hey!'),
(2, 12, 'comment', '2025-04-10 05:15:00', 'Not much!'),
(1, 13, 'comment', '2025-04-10 15:20:00', 'I saw it!'),
(1, 14, 'comment', '2025-04-10 15:45:00', 'LOL don’t lick it!'),
(2, 14, 'reribb', '2025-04-10 15:50:00', NULL),
(1, 15, 'comment', '2025-04-10 15:05:00', 'Cryptid for sure!'),
(2, 15, 'reribb', '2025-04-10 15:10:00', NULL);

-- EVENTS
INSERT INTO events (post_id, event_time, location, host_org, rsvp_count, max_capacity) VALUES
(16, '2025-04-18 20:00:00', 'South Quad', 'Student Activities Board', 63, NULL),
(17, '2025-04-27 16:00:00', 'Commons Game Room', 'Strategy Club', 0, 18);